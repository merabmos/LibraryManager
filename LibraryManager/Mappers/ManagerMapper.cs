using AutoMapper;
using Domain.Entities;
using LibraryManager.Models.EmployeeModels;
using LibraryManager.Models.Sector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Mappers
{
    public class ManagerMapper : Profile
    {
        public ManagerMapper()
        {
            CreateMap<EmployeeVM, Employee>();
            CreateMap<Employee, EmployeeVM>();
            CreateMap<RegisterVM, Employee>();
            CreateMap<Employee, RegisterVM>();
            CreateMap<DetailsVM, Employee>();
            CreateMap<Employee, DetailsVM>();
            CreateMap<Sector,CreateSectorVM>();
            CreateMap<CreateSectorVM, Sector>();
            CreateMap<Sector, SectorVM>();
            CreateMap<SectorVM, Sector>();
            CreateMap<Sector, EditSectorVM>();
            CreateMap<EditSectorVM, Sector>();
        }
    }
}
