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

            List<Sector> GetByCreators = await _filter.GetListById(filter.CreatorId, "CreatorEmployeeId", sectors);
            List<Sector> GetByModifiers = await _filter.GetListById(filter.ModifierId, "ModifierEmployeeId", sectors);
            List<Sector> GetByBetweenModifyDate = await _filter.FilterInBetweenDates(filter.ModifyStartDate,
                filter.ModifyEndDate, "ModifyDate", sectors);
            List<Sector> GetByBetweenInsertDate = await _filter.FilterInBetweenDates(filter.InsertStartDate,
                filter.InsertEndDate, "InsertDate", sectors);

            var commons = sectors.Select(s => s.Id)
                .Intersect(GetByModifiers.Select(s2 => s2.Id).ToList())
                .Intersect(GetByCreators.Select(s1 => s1.Id).ToList())
                .Intersect(GetByBetweenInsertDate.Select(s3 => s3.Id).ToList())
                .Intersect(GetByBetweenModifyDate.Select(s4 => s4.Id).ToList()).ToList();
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