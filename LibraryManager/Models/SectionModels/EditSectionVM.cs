using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Models.SectionModels
{
    public class EditSectionVM
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public int SectorId { get; set; }

        public List<SelectListItem> SectorsSelectList { get; set; } = new List<SelectListItem>();
    }
}