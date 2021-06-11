using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers;
using LibraryManager.Models.BookModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.Controllers
{
    public class BookController : Controller
    {
        private readonly BookManager _bookManager;
        private readonly IMapper _mapper;
        private readonly GenreManager _genreManager;
        private readonly UserManager<Employee> _userManager;
        public BookController(BookManager bookManager, IMapper mapper, GenreManager genreManager, UserManager<Employee> userManager)
        {
            _bookManager = bookManager;
            _mapper = mapper;
            _genreManager = genreManager;
            _userManager = userManager;
        }
        

        public async Task<List<Genre>> JsonMethodForGenre(int genreId)
        {
                var genre =  await _genreManager.GetByIdAsync(genreId);
                CreateBookVM.GenreIds().Add(genre.Id);
                return _genreManager.GetAll().ToList();    
        }
        public ActionResult Index()
        {
            return View();
        }
            
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBookVM model)
        {
            model.CurrentCount = model.TotalCount;
            var mapp = _mapper.Map<Book>(model);
            mapp.CreatorId = _userManager.GetUserId(User);
            _bookManager.Insert(mapp);
            return RedirectToAction("AddGenreToBook",new {Id = mapp.Id});
        }
        
        [HttpGet]
        public ActionResult AddGenreToBook(int Id)
        {
            CreateBookVM model = new CreateBookVM();
            model.GenresSelectList.AddRange(_genreManager.GetGenresSelectList());
            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddGenreToBook(int Id ,CreateBookVM model)
        {   
            _bookManager.AddGenreToBook(Id,_userManager.GetUserId(User),model);
            return View();
        }

    }
}
