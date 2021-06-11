using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManager.Models.FilterModels;
using LibraryManager.Models.SectorModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Models.GenreModels
{
    public class GenreVM : FilterVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InsertDate { get; set; }
        public string ModifyDate { get; set; }
        [Display(Name = "Creator Employee")]
        public string CreatorEmployee { get; set; }
        [Display(Name = "Modifier Employee")]
        public string ModifierEmployee { get; set; }
        
        public List<GenreVM> Genres { get; set; } = new List<GenreVM>();
        public List<SelectListItem> CreatorEmployeesSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ModifierEmployeesSelectList { get; set; } = new List<SelectListItem>();
    }
}