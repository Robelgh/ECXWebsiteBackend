using ECX.Website.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
    public class CryptographyRepository : ICryptographyRepository
    {
        public string DecryptString(string encryptedString, string key)
        {
           return "DecryptedString";
        }

        public string EncryptString(string encryptedString, string key)
        {
            throw new NotImplementedException();
        }
    }
}
