using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndyBooks.Models;
using IndyBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IndyBooks.Controllers
{
    public class AdminController : Controller
    {
        private IndyBooksDataContext _db;
        public AdminController(IndyBooksDataContext db) { _db = db; }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel search)
        {
            //Full Collection Search
            IQueryable<Book> foundBooks = _db.Books.Include(b => b.Author) ; //<<<< Why do you need this

            //Partial Title Search
            if (search.Title != null)
            {
                foundBooks = foundBooks
                             .Where(b => b.Title.Contains(search.Title))
                             .OrderBy(b => b.Title)
                             ;
            }

            //Author's Last Name Search
            if (search.AuthorLastName != null)
            {
                //TODO:Create lamda expression to filter collection using the Name property of the Book's Author entity
                foundBooks = foundBooks
                             .Include(b => b.Author);
                             
                             
            }
            //Priced Between Search (min and max price entered)
            if (search.MinPrice > 0 && search.MaxPrice > 0)
            {
                foundBooks = foundBooks 
                            .Where(b => b.Price >= search.MinPrice && b.Price <= search.MaxPrice)
                             .OrderByDescending(b => b.Price);
            }
            //Composite Search Results
            return View("SearchResults", foundBooks);
        }
    }
}
