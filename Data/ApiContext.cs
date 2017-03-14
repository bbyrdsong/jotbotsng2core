using Microsoft.EntityFrameworkCore;
using JotBotNg2Core.Models;
using JotBotNg2Core.Lib;
using System.Linq;
using System;
using System.Reflection;

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
        public DbSet<CodeArticle> CodeArticles { get; set; }
#endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Directory>().ToTable("Directory");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<QuickNote>().ToTable("QuickNote");
            modelBuilder.Entity<WorkTask>().ToTable("WorkTask");
            modelBuilder.Entity<CodeArticle>().ToTable("CodeArticle");
        }

#region IDataContext implementation
        public T Insert<T>(T entity) where T: class, ISupportIdentity
        {
            ApplyAuditTrail(ref entity);
            return base.Set<T>().Add(entity).Entity;
        }

        public void Delete<T>(int id) where T: class, ISupportIdentity
        {
            var dbEntity = Find<T>(id);
            base.Set<T>().Remove(dbEntity);
        }

        public T Find<T>(int id) where T: class, ISupportIdentity
        {
            var result = base.Set<T>().FirstOrDefault(o => o.Id == id);
            return result;
        }

        public IQueryable<T> GetAll<T>() where T: class, ISupportIdentity
        {
            return base.Set<T>();
        }

        public T Modify<T>(T entity, int id) where T: class, ISupportIdentity
        {
            var dbEntity = Find<T>(id);
            MapPropertyValues(entity, ref dbEntity);
            ApplyAuditTrail(ref dbEntity);
            return base.Set<T>().Update(dbEntity).Entity;
        }     

        public int Save()
        {
            return base.SaveChanges();
        }   
#endregion

        private void ApplyAuditTrail<T>(ref T entity) where T: class
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
        }

        private void MapPropertyValues<TSource, TTarget>(TSource source, ref TTarget target) {
            var properties = source.GetType().GetProperties();
            foreach(PropertyInfo pi in properties.Where(p => p.Name != "Id" && p.Name != "Meta"))
            {
                var propVal = pi.GetValue(source, null);
                target.GetType().GetProperty(pi.Name).SetValue(target, propVal, null);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}