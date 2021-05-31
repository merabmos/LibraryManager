using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManager.Models.SearchModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Models.SectionModels
{
    public class SectionVM : FilterVM
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

        public int SectorId { get; set; }

        public List<SectionVM> Sections = new List<SectionVM>();
        public List<SelectListItem> SectorsSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> CreatorEmployeesSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ModifierEmployeesSelectList { get; set; } = new List<SelectListItem>();
    }
}