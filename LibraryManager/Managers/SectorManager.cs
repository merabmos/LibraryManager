using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Models.SearchModels;
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

        public async Task<List<Sector>> FilterAsync(FilterVM filter)
        {
            return await Task.Run(() =>
            {
                DateTime insertStartDate;
                DateTime insertEndDate;
                DateTime modifyStartDate;
                DateTime modifyEndDate;
                List<Sector> sectors = new List<Sector>();
                if (filter.CreatorId.Count() != 0)
                    sectors = _context.Sectors.Where(o => o.CreatorEmployeeId == filter.CreatorId && o.DeleteDate == null).ToList();
                if (filter.ModifierId.Count() != 0 && sectors.Count() != 0)
                    sectors = sectors.Where(o => o.ModifierEmployeeId == filter.ModifierId && o.DeleteDate == null).ToList();
                else
                {
                    if (filter.ModifierId.Count() != 0)
                    {
                        sectors = _context.Sectors.Where(o => o.ModifierEmployeeId == filter.ModifierId && o.DeleteDate == null).ToList();
                    }
                }

                if (sectors.Count() != 0)
                {
                    if (filter.InsertStartDate.Count() != 0)
                    {
                        insertStartDate = DateTime.Parse(filter.InsertStartDate);
                        sectors = sectors.Where(o => o.InsertDate >= insertStartDate && o.DeleteDate == null).ToList();
                    }
                }
                else
                {
                    if (filter.InsertStartDate.Count() != 0)
                    {
                        insertStartDate = DateTime.Parse(filter.InsertStartDate);
                        sectors = _context.Sectors.Where(o => o.InsertDate >= insertStartDate && o.DeleteDate == null).ToList();
                    }
                }

                if (sectors.Count() != 0)
                {
                    if (filter.InsertStartDate.Count() != 0 && filter.InsertEndDate.Count() != 0)
                    {
                        insertEndDate = DateTime.Parse(filter.InsertEndDate);
                        sectors = sectors.Where(o => o.InsertDate <= insertEndDate && o.DeleteDate == null).ToList();
                    }
                }
                else
                {
                    if (filter.InsertStartDate.Count() != 0 && filter.InsertEndDate.Count() != 0)
                    {
                        insertEndDate = DateTime.Parse(filter.InsertEndDate);
                        sectors = _context.Sectors.Where(o => o.InsertDate <= insertEndDate && o.DeleteDate == null).ToList();
                    }
                }
                if (sectors.Count() != 0)
                {
                    if (filter.ModifyStartDate.Count() != 0)
                    {
                        modifyStartDate = DateTime.Parse(filter.ModifyStartDate);
                        sectors = sectors.Where(o => o.ModifyDate >= modifyStartDate && o.DeleteDate == null).ToList();
                    }
                }
                else
                {
                    if (filter.ModifyStartDate.Count() != 0 && filter.ModifyEndDate.Count() != 0)
                    {
                        modifyEndDate = DateTime.Parse(filter.ModifyEndDate);
                        sectors = _context.Sectors.Where(o => o.InsertDate <= modifyEndDate && o.DeleteDate == null).ToList();
                    }
                }

                return sectors;
            });
        }

        public async Task<List<Sector>> FindBySearchAsync(object search)
        {
            return await Task.Run(() =>
            {
                return _context.Sectors.Where(o => o.Name.Contains((string)search)).ToList();
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
