using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using LibraryManager.Models.FilterModels;
using LibraryManager.Models.SectionModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Models.BooksShelfModels
{
    public class BooksShelfVM : FilterVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InsertDate { get; set; }
        public string ModifyDate { get; set; }
        [Display(Name = "Creator Employee")]
        public string CreatorEmployee { get; set; }
        [Display(Name = "Modifier Employee")]
        public string ModifierEmployee { get; set; }
        public string Sector { get; set; }
        public string Section { get; set; }
        public int SectionId { get; set; }
        public int SectorId { get; set; }

        /*public List<Sector> Sectors = new List<Sector>();*/
        public   List<BooksShelfVM> BooksShelves = new List<BooksShelfVM>();
        public List<SelectListItem> SectionsSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> SectorsSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> CreatorEmployeesSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ModifierEmployeesSelectList { get; set; } = new List<SelectListItem>();
    }
}