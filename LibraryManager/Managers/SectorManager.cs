using Database;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Managers
{
    public class SectorManager : Repository<Sector>, ISectorManager
    {
        private readonly LibraryManagerDBContext _context;
        public SectorManager(LibraryManagerDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Sector>> FindBySearchAsync(object search)
        {
            return await Task.Run(() =>
            {
                return _context.Sectors.Where(o => o.Name.Contains((string)search) && o.DeleteDate == null).ToList();
            });
        }

        public override async Task DeleteAsync(object id)
        {
            var sector = await _context.Sectors.FindAsync(id);
            if (sector != null)
            {
                sector.DeleteDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
