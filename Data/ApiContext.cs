using Microsoft.EntityFrameworkCore;
using JotBotNg2Core.Models;
using JotBotNg2Core.Lib;
using System.Linq;
using System;

namespace JotBotNg2Core.Data
{
    public class ApiDbContext : DbContext, IDataContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

#region DbSets
        public DbSet<Directory> Directory { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<QuickNote> QuickNotes { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }
#endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Directory>().ToTable("Directory");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<QuickNote>().ToTable("QuickNote");
            modelBuilder.Entity<WorkTask>().ToTable("WorkTask");
        }

#region IDataContext implementation
        public T Insert<T>(T entity) where T: class, ISupportIdentity
        {
            var audit = ApplyAuditTrail(entity);
            return base.Set<T>().Add(audit).Entity;
        }

        public void Delete<T>(T entity) where T: class, ISupportIdentity
        {
            base.Set<T>().Remove(entity);
        }

        public T Find<T>(int id) where T: class, ISupportIdentity
        {
            return base.Set<T>().FirstOrDefault(o => o.Id == id);
        }

        public IQueryable<T> GetAll<T>() where T: class, ISupportIdentity
        {
            return base.Set<T>();
        }

        public T Modify<T>(T entity) where T: class, ISupportIdentity
        {
            var audit = ApplyAuditTrail(entity);
            return base.Set<T>().Update(audit).Entity;
        }     

        public int Save()
        {
            return base.SaveChanges();
        }   
#endregion

        private T ApplyAuditTrail<T>(T entity) where T: class
        {
            if (entity is IAuditable)
            {
                var user = "JotBotUser"; // TODO: Get authenticated user...
                var audit = entity as IAuditable;
                if (string.IsNullOrEmpty(audit.CreatedBy))
                {
                    audit.CreatedBy = user;
                    audit.CreatedDate = DateTime.Now;
                }
                else{
                    audit.ModifiedBy =user;
                    audit.ModifiedDate = DateTime.Now;
                }
                entity = audit as T;
            }

            return entity;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}