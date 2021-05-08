using Database;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private LibraryManagerDBContext _context = null;
        private DbSet<T> table = null;
       
        public Repository(LibraryManagerDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
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
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();

        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            Save();
        }

        private void Save()
        {
            _context.SaveChanges();
        }
    }
}
