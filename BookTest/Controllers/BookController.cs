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
		IBookService _context;
        public BookController(IBookService context)
        {
			_context = context;
		}

        public async Task<IActionResult> Index(string author, string name, 
            DateTime datePublic, int page = 1, 
            SortBook sortOrder = SortBook.NameAsc)
        {
            var viewModel = await _context.GetAllAsync(author, name, datePublic,
                page, sortOrder);

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
			await _context.AddAsync(book);
			return RedirectToAction(nameof(Index));
		}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                Book? book = await _context.GetByIdAsync(id);
                if (book != null)
                {
                    await _context.DeleteAsync(book);
                    return RedirectToAction(nameof(Index));
                }
			}
			return View("NotFound");
		}

        public async Task<IActionResult> Edit(int id)
        {
            if (id != null)
            {
                Book? book = await _context.GetByIdAsync(id);
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
            await _context.UpdateAsync(book);
            return RedirectToAction(nameof(Index));
        }

		public async Task<IActionResult> Details(int id)
		{
			if (id != null)
			{
				Book? book = await _context.GetByIdAsync(id);
				if (book != null)
				{
					return View(book);
				}
			}
			return View("NotFound");
		}
	}
}
