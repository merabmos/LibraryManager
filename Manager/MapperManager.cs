using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.DTOModels;

namespace Manager
{
    public class MapperManager : Profile
    {
        public MapperManager()
        {
            CreateMap<Employee, EmployeeRegisterDTO>();
        }
    }
}
