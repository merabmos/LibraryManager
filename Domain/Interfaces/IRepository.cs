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
        
        Task<T> GetByIdAsync(object id);
        
        void Insert(T obj);
        void Update(T obj);
        void Delete(T entity);
        
        void Save();
        Task Update_DeleteDate_ByIdAsync(object id);
        List<SelectListItem> GetEmployeesSelectList();
        List<SelectListItem> GetEntitiesSelectList(List<T> entities);
    }
}
