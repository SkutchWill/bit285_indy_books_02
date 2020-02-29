using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndyBooks.Models;
using IndyBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
                             .Where(b => b.Author.Name.Contains(search.AuthorLastName));
                             
                             
            }
            //Priced Between Search (min and max price entered)
            if (search.MinPrice > 0 && search.MaxPrice > 0)
            {
                foundBooks = foundBooks
                             .Where(b => b.Price >= search.MinPrice && b.Price <= search.MaxPrice)
                             //.GroupBy(b => b.Author.Name)
                             //.Select(b => b.First())
                             //I could not get the Distinct function to work so I tried this as a work around but it is giving me errors as well.
                             .OrderByDescending(b => b.Price);                
            }
            if (search.HighPrice)
            {
                foundBooks = foundBooks.First(IGrouping(Price, foundBooks));
                
            }

            //Composite Search Results
            return View("SearchResults", foundBooks);
        }
    }
}
