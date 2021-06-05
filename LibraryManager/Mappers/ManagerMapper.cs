using AutoMapper;
using Domain.Entities;
using LibraryManager.Models.BooksShelfModels;
using LibraryManager.Models.EmployeeModels;
using LibraryManager.Models.SectorModels;
using LibraryManager.Models.SectionModels;

namespace LibraryManager.Mappers
{
    public class ManagerMapper : Profile
    {
        public ManagerMapper()
        {
            //EMPLOYEE
            CreateMap<EmployeeVM, Employee>();
            CreateMap<Employee, EmployeeVM>();
            CreateMap<RegisterVM, Employee>();
            CreateMap<Employee, RegisterVM>();
            CreateMap<DetailsVM, Employee>();
            CreateMap<Employee, DetailsVM>();
         
            //SECTOR
            CreateMap<Sector,CreateSectorVM>();
            CreateMap<CreateSectorVM, Sector>();
            CreateMap<Sector, SectorVM>();
            CreateMap<SectorVM, Sector>();
            CreateMap<Sector, EditSectorVM>();
            CreateMap<EditSectorVM, Sector>();
            
            //SECTION
            CreateMap<Section, SectionVM>();
            CreateMap<SectionVM, Section>();
            CreateMap<CreateSectionVM,Section>();
            CreateMap<Section, CreateSectionVM>();
            CreateMap<EditSectionVM,Section>();
            CreateMap<Section, EditSectionVM>();
            
            //BOOKS SHELF
            CreateMap<BooksShelf, BooksShelfVM>();
            CreateMap<BooksShelfVM, BooksShelf>();
            CreateMap<CreateBooksShelfVM,BooksShelf>();
            CreateMap<BooksShelf, CreateBooksShelfVM>();
        }
    }
}
