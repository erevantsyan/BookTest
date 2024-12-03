
using Microsoft.EntityFrameworkCore;

namespace BookTest.Models
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _context;

		public BookService(IBookRepository context)
		{
			_context = context;
		}

		public async Task AddAsync(Book book)
		{
			await _context.AddAsync(book);
		}

		public async Task DeleteAsync(Book book)
		{
			await _context.DeleteAsync(book);
		}

		public async Task<IndexViewModel> GetAllAsync(string author, string name, 
			DateTime datePublic, int page, SortBook sortOrder)
		{
			int pageSize = 3;

			var count = await _context.CountAsync();
			var books = await _context.GetAllAsync(author, name, datePublic,
				sortOrder, page, pageSize);

			IndexViewModel viewModel = new IndexViewModel(
				books,
				new PageViewModel(count, page, pageSize),
				new FilterViewModel(author, name, datePublic),
				new SortViewModel(sortOrder)
			);

			return viewModel;
		}

		public async Task<Book?> GetByIdAsync(int? id)
		{
			return await _context.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Book newBook)
		{
			await _context.UpdateAsync(newBook);
		}
	}
}
