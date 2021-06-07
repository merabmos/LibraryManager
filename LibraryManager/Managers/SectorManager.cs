﻿using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using LibraryManager.Models.SectorModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Managers
{
    public class SectorManager
    {
        private readonly IFilter<Sector> _filter;
        private readonly LibraryManagerDBContext _context;
        private readonly IRepository<Sector> _repository;

        public SectorManager(LibraryManagerDBContext context, IFilter<Sector> filter, IRepository<Sector> repository)
        {
            _context = context;
            _filter = filter;
            _repository = repository;
        }

        public async Task<List<Sector>> FilterAsync(SectorVM filter)
        {
            var sectors = _context.Sectors.Where(o => o.DeleteDate == null).ToList();
            List<Sector> GetByCreators = filter.CreatorId != null
                ? await _filter.GetListByValue(filter.CreatorId, "CreatorId", sectors)
                : null;
            List<Sector> GetByModifiers = filter.ModifierId != null
                ? await _filter.GetListByValue(filter.ModifierId, "ModifierId", sectors)
                : null;

            List<Sector> GetByBetweenInsertDate = filter.InsertStartDate != null || filter.InsertEndDate != null
                ? await _filter.FilterInBetweenDates(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", sectors)
                : null;

            List<Sector> GetByBetweenModifyDate = filter.ModifyStartDate != null || filter.ModifyEndDate != null
                ? await _filter.FilterInBetweenDates(filter.ModifyStartDate,
                    filter.ModifyEndDate, "ModifyDate", sectors)
                : null;

            var FilteredSectors = _filter.Intersect(sectors, GetByBetweenInsertDate, GetByCreators,
                GetByModifiers,
                GetByBetweenModifyDate).ToList();

            return FilteredSectors;
        }

        public async Task<Sector> GetSectorByIdAsync(int? id)
        {
            return await _context.Sectors.FindAsync(id);
        }


        public List<SelectListItem> GetSectorsSelectList()
        {
            return _repository.GetAliveEntitiesSelectList(_context.Sectors.Where(o => o.DeleteDate == null).ToList());
        }
    }
}