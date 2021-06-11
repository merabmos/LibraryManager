using AutoMapper;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.BookModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.Managers
{
    public class BookManager : Repository<Book>
    {
        private readonly IFilter<Genre> _filter;
        private readonly LibraryManagerDBContext _context;
        private readonly IRepository<Genre> _repository;
        private readonly IMapper _mapper;

        public BookManager(LibraryManagerDBContext context, UserManager<Employee> userManager, IFilter<Genre> filter,
            IRepository<Genre> repository, IMapper mapper) : base(context,
            userManager)
        {
            _context = context;
            _filter = filter;
            _repository = repository;
            _mapper = mapper;
        }

        public void AddGenreToBook(int bookId, string userId ,CreateBookVM model)
        {
            var mapp = _mapper.Map<BooksGenre>(model);
            mapp.BookId = bookId;
            mapp.CreatorId = userId;
            _context.BooksGenres.Add(mapp);
        }
    }
}