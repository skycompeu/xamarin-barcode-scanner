using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OmniReader.Data.Database.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> Find(Expression<Func<T, bool>> expression);


        /// <summary>
        ///     Inserts the given object (and updates its auto incremented primary key if it
        ///     has one). The return value is the number of rows added to the table.
        ///     
        ///     Zwraca: The number of rows added to the table.        
        int Insert(T entity);


        /// <summary>
        ///     Updates all of the columns of a table using the specified object except for its
        ///     primary key. The object is required to have a primary key.
        ///     
        ///     The number of rows updated.
        int Update(T entity);

        

        //int  Delete(T entity);
        int DeleteAll();


        TableQuery<T> GetTable();


        //bool TransactionGroupDelete(List<T> entities);
        //bool GroupInsert(List<T> entities);


    }
}
