using Database;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Filters;

namespace LibraryManager.Managers.Main
{
    public class Repository<T> : Filter<T>, IRepository<T> where T : class
    {
        private readonly LibraryManagerDBContext _context;
        private readonly DbSet<T> _table;
        private readonly UserManager<Employee> _userManager;
        private readonly IFilter<T> _filter;

        public Repository(LibraryManagerDBContext context,
            UserManager<Employee> userManager,
            IFilter<T> filter) : base(context)
        {
            _context = context;
            _table = _context.Set<T>();
            _userManager = userManager;
            _filter = filter;
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
            Save();
        }

        public void Update(T obj)
        {
            if (obj != null)
            {
                _table.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
                Save();
            }
        }

        // Delete Record
        public void Delete(T existing)
        {
            _table.Remove(existing);
            Save();
        }
        
        // Update DeleteDate In Table
        public async Task Update_DeleteDate_ByIdAsync(object id)
        {
            var entity = await _table.FindAsync(id);
            if (entity != null)
            {
                entity.GetType().GetProperty("DeleteDate")?.SetValue(entity, DateTime.Now);
                Update(entity);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        
        ///////////////////////////////////////////////////////////////////////// 
        
        public List<SelectListItem> GetEntitiesSelectList(List<T> list = null)
        {
            
            list ??= _table.ToList().Where(o => o.GetType().GetProperty("DeleteDate")?.GetValue(o) == null).ToList();

            List<SelectListItem> employeeSelectList = new List<SelectListItem>();
            
            try
            {
                foreach (var item in list)
                {
                    object obj = item;
                    var GetNameProperty = item.GetType().GetProperty("Name");
                    var GetIdProperty = item.GetType().GetProperty("Id");
                    object valueOfName = GetNameProperty?.GetValue(obj, null);
                    object valueOfId = GetIdProperty?.GetValue(obj, null);
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = valueOfName?.ToString(), Value = valueOfId?.ToString()
                    };
                    employeeSelectList.Add(selectListItem);
                }

                return employeeSelectList;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        
        public List<SelectListItem> GetEmployeesSelectList()
        {
            List<SelectListItem> employeeSelectList = new List<SelectListItem>();
            foreach (var employee in _userManager.Users)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = employee.FirstName + " " + employee.LastName, Value = employee.Id
                };
                employeeSelectList.Add(selectListItem);
            }

            return employeeSelectList;
        }
  

       
  
    }
}