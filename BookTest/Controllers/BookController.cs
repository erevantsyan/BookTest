using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTest.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Components.Web;

namespace BookTest.Controllers
{
    public class BookController : Controller
    {
        ApplicationContext db;
        public BookController(ApplicationContext context)
        {
			db = context;
		}

        public async Task<IActionResult> Index(string author, string name, 
            DateTime datePublic, int page = 1, 
            SortBook sortOrder = SortBook.NameAsc)
        {
            int pageSize = 3;

            IQueryable<Book> books = db.Books;

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(p => p.Author!.Contains(author));
            }
            if (!string.IsNullOrEmpty(name))
            {
                books = books.Where(p => p.Name!.Contains(name));
            }
            if (datePublic != DateTime.MinValue)
            {
                books = books.Where(p => p.DatePublic == datePublic);
            }

            switch(sortOrder)
			{
                case SortBook.NameDesc:
                    books = books.OrderByDescending(s => s.Name);
                    break;
                case SortBook.AuthorAsc:
					books = books.OrderBy(s => s.Author);
                    break;
                case SortBook.AuthorDesc:
					books = books.OrderByDescending(s => s.Author);
                    break;
                case SortBook.DateAsc:
					books = books.OrderBy(s => s.DatePublic);
                    break;
                case SortBook.DateDesc:
					books = books.OrderByDescending(s => s.DatePublic);
                    break;
                default:
					books = books.OrderBy(s => s.Name);
                    break;
            }

            var count = await books.CountAsync();
            var items = await books.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(author, name, datePublic),
                new SortViewModel(sortOrder)
            );

			return View(viewModel);
        }

		public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
			if (!ModelState.IsValid)
			{
				return View(book);
			}
			db.Books.Add(book);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
		}

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Book? book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
			}
			return View("NotFound");
		}

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Book? book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    return View(book);
                }
            }
			return View("NotFound");
		}

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

		public async Task<IActionResult> Details(int? id)
		{
			if (id != null)
			{
				Book? book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
				if (book != null)
				{
					return View(book);
				}
			}
			return View("NotFound");
		}
	}
}
