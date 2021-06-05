using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Models.BooksShelfModels
{
    public class CreateBooksShelfVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public int SectorId { get; set; }
        public int SectionId { get; set; }

        public List<SelectListItem> SectorsSelectList = new List<SelectListItem>();
        public List<SelectListItem> SectionsSelectList = new List<SelectListItem>();
        
    }
}