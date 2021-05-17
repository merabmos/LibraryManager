using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.SearchModels
{
    public class FilterVM
    {
        public string CreatorId { get; set; }
        public string ModifierId { get; set; } 
        public string InsertStartDate { get; set; }
        public string InsertEndDate { get; set; }
        public string ModifyStartDate { get; set; }
        public string ModifyEndDate { get; set; }
    }
}
