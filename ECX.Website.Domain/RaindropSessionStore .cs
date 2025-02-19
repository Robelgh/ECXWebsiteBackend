using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Domain
{
    public class RaindropSessionStore : Microsoft.AspNetCore.Authentication.Cookies.ITicketStore
    {

        public System.Threading.ReaderWriterLockSlim storeLock = new();
        public Microsoft.Extensions.Caching.Distributed.IDistributedCache cache = null;
        public double SessionTimeout = 10;
        public RaindropSessionStore()
        {

        }

        public Task RemoveAsync(string key)
        {
            storeLock.EnterWriteLock();
            try
            {
                if (cache.Get(key) != null)
                {
                    cache.Remove(key);
                }

                return Task.FromResult(0);
            }
            finally
            {
                storeLock.ExitWriteLock();
            }
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            storeLock.EnterReadLock();
            try
            {
                TicketSerializer s = new();
                cache.Set(key, s.Serialize(ticket), new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(SessionTimeout)
                });
                return Task.FromResult(false);
            }
            finally
            {
                storeLock.ExitReadLock();
            }
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            storeLock.EnterReadLock();
            try
            {
                if (cache.Get(key) != null)
                {
                    TicketSerializer s = new();
                    var ticket = s.Deserialize(cache.Get(key));
                    return Task.FromResult(ticket);
                }
                else
                {
                    return Task.FromResult((AuthenticationTicket)null!);
                }
            }
            finally
            {
                storeLock.ExitReadLock();
            }
        }

        public Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            storeLock.EnterWriteLock();
            try
            {
                var key = System.Text.Json.JsonSerializer.Deserialize<LoginTrader>(ticket.Principal.Claims.ToList()[0].ToString().Replace("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier: ", "")).Uniqueidentifier.ToString() + "-" +
                    System.Text.Json.JsonSerializer.Deserialize<LoginTrader>(ticket.Principal.Claims.ToList()[0].ToString().Replace("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier: ", "")).SessionID;
                TicketSerializer s = new();
                cache.Set(key, s.Serialize(ticket), new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(SessionTimeout)
                });
                return Task.FromResult(key);
            }
            catch
            {
                throw new Exception("Failed to add entry to the RaindropSessionStore.");
            }
            finally
            {
                storeLock.ExitWriteLock();
            }
        }
    }
}
