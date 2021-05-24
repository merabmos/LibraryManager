using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManager.Managers
{
    public class SectorManager : Repository<Sector>, ISectorManager
    {
        private readonly IFilter<Sector> _filter;
        private readonly LibraryManagerDBContext _context;

        public SectorManager(LibraryManagerDBContext context, IFilter<Sector> filter) : base(context)
        {
            _context = context;
            _filter = filter;
        }


        public async Task<List<Sector>> FilterAsync(FilterVM filter)
        {
            var sectors = _context.Sectors.Where(o => o.DeleteDate == null).ToList();

            List<Sector> GetByBetweenInsertDate = filter.InsertStartDate.Length != 0 || filter.InsertEndDate.Length != 0
                ? await _filter.FilterInBetweenDates(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", sectors)
                : new List<Sector>();
            List<Sector> GetByCreators = filter.CreatorId.Length != 0
                ? await _filter.GetListById(filter.CreatorId, "CreatorEmployeeId", sectors)
                : new List<Sector>();
            List<Sector> GetByModifiers = filter.ModifierId.Length != 0
                ? await _filter.GetListById(filter.ModifierId, "ModifierEmployeeId", sectors)
                : new List<Sector>();
            List<Sector> GetByBetweenModifyDate = filter.ModifyStartDate.Length != 0 || filter.ModifyEndDate.Length != 0
                ? await _filter.FilterInBetweenDates(filter.ModifyStartDate,
                    filter.ModifyEndDate, "ModifyDate", sectors)
                : new List<Sector>();

                var FilteredSectors = _filter.IntersectAllIfEmpty(sectors, GetByBetweenInsertDate, GetByCreators,
                    GetByModifiers,
                    GetByBetweenModifyDate);

            sectors = new List<Sector>();

            foreach (var item in FilteredSectors)
            {
                sectors.Add(item);
            }

            return sectors;
        }

        public async Task<List<Sector>> FindBySearchAsync(object search)
        {
            return await Task.Run(() =>
            {
                return _context.Sectors.Where(o => o.Name.Contains((string) search)).ToList();
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