using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.SearchModels;
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

        public SectorManager(LibraryManagerDBContext context, IFilter<Sector> filter)
        {
            _context = context;
            _filter = filter;
        }


        public async Task<List<Sector>> FilterAsync(SectorVM filter)
        {
            var sectors = _context.Sectors.Where(o => o.DeleteDate == null).ToList();
            #region Commented

            /*var GetPropertiesWithId = typeof(FilterVM).GetProperties().Where(o => o.Name.EndsWith("Id")).ToList();
            foreach (var vari in GetPropertiesWithId)
            {
                   var manaxe = vari.Name.Replace("Id",string.Empty);
                    List<Sector> GetById = vari.GetValue(filter).ToString().Length != 0
                        ? await _filter.GetListById(filter.CreatorId, $"{manaxe}Id", sectors)
                        : null;
            }*/

            #endregion
            List<Sector> GetByCreators = filter.CreatorId != null
                ? await _filter.GetListBy(filter.CreatorId, "CreatorId", sectors)
                : null;
            List<Sector> GetByModifiers = filter.ModifierId != null
                ? await _filter.GetListBy(filter.ModifierId, "ModifierId", sectors)
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

        public async Task<Sector> GetSectorById(int? id)
        {
            return await _context.Sectors.FindAsync(id);
        }

        public async Task<List<Sector>> FindBySearchAsync(object search)
        {
            return await Task.Run(() =>
            {
                // && o.DeleteDate == null
                return _context.Sectors.Where(o => o.Name.Contains((string) search)).ToList();
            });
        }

        public List<SelectListItem> GetSectorsSelectList()
        {
            List<SelectListItem> employeeSelectList = new List<SelectListItem>();
            foreach (var sector in _context.Sectors.Where(o => o.DeleteDate == null))
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = sector.Name;
                selectListItem.Value = sector.Id.ToString();
                employeeSelectList.Add(selectListItem);
            }

            return employeeSelectList;
        }

        public List<SelectListItem> GetSectorsSelectList(int? Id)
        {
            List<SelectListItem> employeeSelectList = new List<SelectListItem>();
            foreach (var sector in _context.Sectors.Where(o => o.DeleteDate == null))
            {
                SelectListItem selectListItem = new SelectListItem();
                if ( _context.Sectors.Find(Id) != null)
                {
                    selectListItem.Selected = true;
                }

                selectListItem.Text = sector.Name;
                selectListItem.Value = sector.Id.ToString();
                employeeSelectList.Add(selectListItem);
            }

            return employeeSelectList;
        }
        
        public async Task RemoveByIdAsync(object id)
        {
            var section =  await _context.Sections.FindAsync(id);
            if (section != null)
            {
                section.DeleteDate = DateTime.Now;
                _context.Sections.Update(section);
                await _context.SaveChangesAsync();
            }
        }
    }
}