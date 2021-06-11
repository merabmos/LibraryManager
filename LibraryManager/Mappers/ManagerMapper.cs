using AutoMapper;
using Domain.Entities;
using LibraryManager.Models.AuthorModels;
using LibraryManager.Models.BookModels;
using LibraryManager.Models.BooksShelfModels;
using LibraryManager.Models.EmployeeModels;
using LibraryManager.Models.GenreModels;
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
            CreateMap<EditBooksShelfVM,BooksShelf>();
            CreateMap<BooksShelf, EditBooksShelfVM>();
            
            //GENRE
            CreateMap<Genre, GenreVM>();
            CreateMap<GenreVM, Genre>();
            CreateMap<CreateGenreVM,Genre>();
            CreateMap<Genre, CreateGenreVM>();
            CreateMap<EditGenreVM,Genre>();
            CreateMap<Genre, EditGenreVM>();
            
            //AUTHOR
            CreateMap<Author, AuthorVM>();
            CreateMap<AuthorVM, Author>();
            CreateMap<CreateAuthorVM,Author>();
            CreateMap<Author, CreateAuthorVM>();
            CreateMap<EditAuthorVM,Author>();
            CreateMap<Author, EditAuthorVM>();
            
            //BOOK
            CreateMap<CreateBookVM,Book>();
            CreateMap<Book, CreateBookVM>();
            CreateMap<CreateBookVM,BooksGenre>();
            CreateMap<BooksGenre, CreateBookVM>();
        }
    }
}
