using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.BookModels
{
    public class CreateBookVM
    {
        List<int> BooksShelfIds = new List<int>();
        List<int> AuthorIds = new List<int>();
        List<int> GenreIds = new List<int>();


        public int GenreId { get; set; }
        
        [Required]
        [DisplayName("Title")]
        public string Name { get; set; }
        
        [Required]
        public int TotalCount { get; set; }
        public int CurrentCount { get; set; }

        public readonly List<SelectListItem> BooksShelvesSelectList = new List<SelectListItem>();
        public readonly List<SelectListItem> SectionsSelectList  = new List<SelectListItem>();
        public readonly List<SelectListItem> SectorsSelectList  = new List<SelectListItem>();
        public readonly List<SelectListItem> AuthorsSelectList  = new List<SelectListItem>();
        public readonly List<SelectListItem> GenresSelectList = new List<SelectListItem>();

    }
}
