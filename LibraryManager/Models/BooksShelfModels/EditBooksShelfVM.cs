using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Models.BooksShelfModels
{
    public class EditBooksShelfVM
    {
        
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        public int SectorId { get; set; }
        
        public int SectionId { get; set; }
        
        [Required]
        public readonly List<SelectListItem> SectorsSelectList = new List<SelectListItem>();
        [Required]
        public readonly List<SelectListItem> SectionsSelectList = new List<SelectListItem>();
    }
}