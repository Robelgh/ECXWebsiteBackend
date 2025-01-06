﻿using ECX.Website.Domain;
using ECX.Website.Domain.Common;
using ECX.Website.Domain.Lookup;
using ECX.Website.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence
{
    public class ECXWebsiteDbContext :DbContext    
    {
        public ECXWebsiteDbContext(DbContextOptions<ECXWebsiteDbContext> options) : base(options)
        {
            
        }
  
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entity.Entity.UpdatedDate = DateTime.Now;
                if(entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedDate = DateTime.Now;
                    entity.Entity.IsActive = true;
                }
                
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public const string DEFAULT_SCHEMA = "ECXWebsite";
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageCatagory> PageCatagories { get; set; }
        public DbSet<BoardOfDirector> BoardOfDirectors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<ContractFile> ContractFiles { get; set; }
        public DbSet<ExternalLink> ExternalLinks { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<MarketData> MarketData { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Research> Researchs { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Facts> Facts { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingDoc> TrainingDocs { get; set; }
        public DbSet<Vacancy> Vacancys { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<ParentLookup> ParentLookups { get; set; }
        public DbSet<SessionSchedule> SessionSchedules { get; set; }
        public DbSet<ComplainFeedBack> complainFeedBacks { get; set; }
        public DbSet<RequestInfo> requestInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECXWebsiteDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new CommodityContractFileETC());
            modelBuilder.ApplyConfiguration(new ParentLookupPageCategoryETC());
            modelBuilder.ApplyConfiguration(new PageCatagoryVacancyETC());
        }



    }
}
