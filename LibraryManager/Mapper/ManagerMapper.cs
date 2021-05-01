using AutoMapper;
using Domain.Entities;
using LibraryManager.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Mapper
{
    public class ManagerMapper : Profile
    {
        public ManagerMapper()
        {
            CreateMap<RegisterVM, Employee>();
            CreateMap<Employee, RegisterVM>();
        }
    }
}
