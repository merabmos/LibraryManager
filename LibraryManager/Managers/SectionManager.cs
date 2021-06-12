using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.EmployeeModels;
using LibraryManager.Models.SectionModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibraryManager.Managers
{
    public class SectionManager : Repository<Section>
    {
        private readonly IFilter<Section> _filter;
        private readonly LibraryManagerDBContext _context;
        private readonly IRepository<Section> _repository; 
        public SectionManager(LibraryManagerDBContext context,
            IFilter<Section> filter,
            IRepository<Section> repository,UserManager<Employee> userManager) :  base(context,userManager,filter)
        {
            _context = context;
            _filter = filter;
            _repository = repository;
        }
        
        public async Task<List<Section>> FilterAsync(SectionVM filter)
        {
            var sections = _context.Sections.Where(o => o.DeleteDate == null).ToList();
           
            List<Section> GetBySectors= filter.SectorId != 0
                ? await _filter.FilterOfEntititesByValue(filter.SectorId, "SectorId", sections)
                : null;
            
            List<Section> GetByCreators = filter.CreatorId != null
                ? await _filter.FilterOfEntititesByValue(filter.CreatorId, "CreatorId", sections)
                : null;
            
            List<Section> GetByModifiers = filter.ModifierId != null
                ? await _filter.FilterOfEntititesByValue(filter.ModifierId, "ModifierId", sections)
                : null;

            List<Section> GetByBetweenInsertDate = filter.InsertStartDate != null || filter.InsertEndDate != null
                ? await _filter.FilterOfDate(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", sections)
                : null;
      
            List<Section> GetByBetweenModifyDate = filter.ModifyStartDate != null || filter.ModifyEndDate != null
                ? await _filter.FilterOfDate(filter.ModifyStartDate,
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
    }
}