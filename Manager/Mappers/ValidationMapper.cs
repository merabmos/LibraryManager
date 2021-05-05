using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Models;

namespace Manager.Mappers
{
    public class ValidationMapper : Profile
    {
        public ValidationMapper()
        {
            CreateMap<Employee, EmployeeVal>();
        }
    }
}
