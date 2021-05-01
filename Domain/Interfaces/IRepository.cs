using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
   public interface IRepository<T> where T : class
    {
        void Insert(T t);
        T Update(T t);
        void Delete(T entity);
        T GetById(int Id);
        List<T> GetAll();
    }
}
