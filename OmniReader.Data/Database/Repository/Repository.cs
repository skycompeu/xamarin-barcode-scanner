using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OmniReader.Data.Database.Repository
{
        
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private SQLite.SQLiteConnection _db;
        private DatabaseContext _manager;
        private object m_lock = new object();
        
        public Repository()
        {
            _manager = new DatabaseContext();
            _db = _manager.GetConnection();
        }
        
        public int Insert(T entity)
        {
            lock (m_lock)
            {
                return _db.Insert(entity, typeof(T));
            }
        }

        //public int Delete(T entity)
        //{
        //    lock (m_lock) {
        //        return _db.Delete<T>(entity);
        //    }
        //}

        public int Delete(long Id)
        {
            lock (m_lock)
            {
                return _db.Delete<T>(Id);
            }
        }
        
        public int Update(T entity)
        {
            lock (m_lock)
            {
                return _db.Update(entity, typeof(T));
            }
        }

        public List<T> GetAll()
        {
            lock (m_lock) {
                return _db.Table<T>().ToList();
            }
        }
        
        public List<T> Find(Expression<Func<T, bool>> expression)
        {
            lock (m_lock) {
                return _db.Table<T>().Where(expression).ToList();
            }
        }
        
        public TableQuery<T> GetTable() {
            lock (m_lock) {
                return _db.Table<T>();
            }
        }


        //public bool TransactionGroupDelete(List<T> entities)
        //{
        //    lock (m_lock)
        //    {
        //        bool ok = true;
                
        //        try
        //        {
        //            _db.BeginTransaction();
                    
        //            foreach (var entity in entities)
        //            {
        //                _db.Delete<T>(entity);
        //            }

        //            _db.Commit();
        //        }
        //        catch (Exception e) 
        //        {
        //            _db.Rollback();
        //            ok = false;
        //            throw new Exception(e.Message);
        //        }

        //        return ok;
        //    }
        //}

        //public bool GroupInsert(List<T> entities)
        //{
        //    lock (m_lock)
        //    {
        //        bool ok = true;

        //        try
        //        {
        //            _db.BeginTransaction();

        //            foreach (var item in entities)
        //            {
        //                _db.Insert(item);
        //            }

        //            _db.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            _db.Rollback();
        //            ok = false;
        //        }

        //        return ok;
        //    }
        //}

        public int DeleteAll()
        {
            return _db.DeleteAll<T>();
        }
    }
}