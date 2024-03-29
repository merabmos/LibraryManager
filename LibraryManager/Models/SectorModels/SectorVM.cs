﻿using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryManager.Models.FilterModels;

namespace LibraryManager.Models.SectorModels
{
    public class SectorVM : FilterVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InsertDate { get; set; }
        public string ModifyDate { get; set; }
        [Display(Name = "Creator Employee")]
        public string CreatorEmployee { get; set; }
        [Display(Name = "Modifier Employee")]
        public string ModifierEmployee { get; set; }
        
        public List<SectorVM> Sectors { get; set; } = new List<SectorVM>();
        public List<SelectListItem> CreatorEmployeesSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ModifierEmployeesSelectList { get; set; } = new List<SelectListItem>();

    }
}
