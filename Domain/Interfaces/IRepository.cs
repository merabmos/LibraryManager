using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
   public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        
        T GetByIdAsync(object id);
        
        void Insert(T obj);
        void Update(T obj);
        void Delete(T entity);
        
        void Save();
        
        List<SelectListItem> GetEmployeesSelectList();
        List<SelectListItem> GetAliveEntitiesSelectList(List<T> entities);
    }
}
