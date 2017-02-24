using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JotBotNg2Core.Lib
{
    public interface IDataContext
    {
        IQueryable<T> GetAll<T>() where T: class, ISupportIdentity;
        T Find<T>(int id) where T: class, ISupportIdentity;
        T Insert<T>(T entity) where T: class, ISupportIdentity;
        T Modify<T>(T entity) where T: class, ISupportIdentity;
        void Delete<T>(T entity) where T: class, ISupportIdentity;
        int Save();
        void Dispose();
    }
}