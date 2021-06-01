using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.SectionModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.Managers
{
    public class SectionManager : Repository<Section>
    {
        private readonly IFilter<Section> _filter;
        private readonly LibraryManagerDBContext _context;

        public SectionManager(LibraryManagerDBContext context, IFilter<Section> filter) : base(context)
        {
            _context = context;
            _filter = filter;
        }
        
        public async Task<List<Section>> FilterAsync(SectionVM filter)
        {
            var sections = _context.Sections.Where(o => o.DeleteDate == null).ToList();
           
            List<Section> GetBySectors= filter.SectorId != 0
                ? await _filter.GetListById(filter.SectorId, "SectorId", sections)
                : null;
            
            List<Section> GetByCreators = filter.CreatorId != null
                ? await _filter.GetListBy(filter.CreatorId, "CreatorId", sections)
                : null;
            
            List<Section> GetByModifiers = filter.ModifierId != null
                ? await _filter.GetListBy(filter.ModifierId, "ModifierId", sections)
                : null;

            List<Section> GetByBetweenInsertDate = filter.InsertStartDate != null || filter.InsertEndDate != null
                ? await _filter.FilterInBetweenDates(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", sections)
                : null;
      
            List<Section> GetByBetweenModifyDate = filter.ModifyStartDate != null || filter.ModifyEndDate != null
                ? await _filter.FilterInBetweenDates(filter.ModifyStartDate,
                    filter.ModifyEndDate, "ModifyDate", sections)
                : null;
                     
            
            var FilteredSections = _filter.Intersect(sections, GetByBetweenInsertDate, GetByCreators,
                GetByModifiers,
                GetByBetweenModifyDate,GetBySectors);

            sections = new List<Section>();

            foreach (var item in FilteredSections)
            {
                sections.Add(item);
            }

            return sections;
        }
        
        public async Task<List<Section>> FilterTableByAsync(object obj,string columnInTable)
        {
            var sections = _context.Sections.ToList();
            List<Section> GetBySectors = obj != null
                ? await _filter.GetListBy(obj,columnInTable, sections)
                : null;
            return GetBySectors;
        }

        public List<Section> FilterLists(params IEnumerable<Section>[] lists)
        {
            var FilteredSections = _filter.Intersect(lists).ToList();
            return FilteredSections;
        }

        public override async Task DeleteAsync(object id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                section.DeleteDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}