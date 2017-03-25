﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Companies.Core.Aggregates;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess.Repositories;

namespace Wilson.Companies.Data.DataAccess
{
    public class CompanyWorkData : ICompanyWorkData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public CompanyWorkData(DbContextOptions<CompanyDbContext> options)
            : this(new CompanyDbContext(options))
        {
        }

        public CompanyWorkData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<InquiryAggregate> Inquiries => this.GetRepository<InquiryAggregate>();

        public IRepository<User> Users => this.GetRepository<User>();

        public int Complete()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return this.dbContext.Set<TEntity>();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(Repository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}