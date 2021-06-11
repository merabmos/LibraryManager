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

namespace LibraryManager.Managers.Main
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryManagerDBContext _context;
        private readonly DbSet<T> table ;
        private readonly UserManager<Employee> _userManager;
   
        public Repository(LibraryManagerDBContext context, UserManager<Employee> userManager)
        {
            _context = context;
            table = _context.Set<T>();
            _userManager = userManager;
        }
  
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            Save();
        }

        public void Update(T obj)
        {
            if (obj != null)
            {
                table.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
                Save();
            }
        }
        
        // Delete Record
        public void Delete(T existing)
        {
            table.Remove(existing);
            Save();
        }
        
        public List<SelectListItem> GetEntitiesSelectList(List<T> lists)
        {
            List<SelectListItem> employeeSelectList = new List<SelectListItem>();
            try
            {
                foreach (var item in lists)
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
        
        // Update DeleteDate In Table
        public async Task Update_DeleteDate_ByIdAsync(object id)
        {
            var entity = await table.FindAsync(id);
            if (entity != null)
            {
                entity.GetType().GetProperty("DeleteDate")?.SetValue(entity,DateTime.Now);
                Update(entity);
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}