using Database;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Managers.Main
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private LibraryManagerDBContext _context = null;
        private DbSet<T> table = null;
        private LibraryManagerDBContext context;
        private readonly UserManager<Employee> _userManager;

        public Repository(LibraryManagerDBContext context, UserManager<Employee> userManager)
        {
            _context = context;
            table = _context.Set<T>();
            _userManager = userManager;
        }

        public Repository(LibraryManagerDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
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

        public virtual async Task DeleteAsync(object id)
        {
            T existing = await table.FindAsync(id);
            if (existing != null)
            {
                table.Remove(existing);
            }
        }

        public List<SelectListItem> GetEmployeesSelectList()
        {
            var users = _userManager.Users;
            List<SelectListItem> employeeSelectList = new List<SelectListItem>();
            foreach (var employee in _userManager.Users)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = employee.FirstName + " " + employee.LastName;
                selectListItem.Value = employee.Id;
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
