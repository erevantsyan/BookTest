
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
			var books = await _context.GetAllAsync();

			if (!string.IsNullOrEmpty(author))
			{
				books = books.Where(p => p.Author.Contains(author));
			}
			if (!string.IsNullOrEmpty(name))
			{
				books = books.Where(p => p.Name.Contains(name));
			}
			if (datePublic != DateTime.MinValue)
			{
				books = books.Where(p => p.DatePublic == datePublic);
			}

			switch (sortOrder)
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

			int pageSize = 3;

			var count = await _context.CountAsync();
			var items = await books
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			IndexViewModel viewModel = new IndexViewModel(
				items,
				new PageViewModel(count, page, pageSize),
				new FilterViewModel(author, name, datePublic),
				new SortViewModel(sortOrder)
			);

			return viewModel;
		}

		public async Task<Book?> GetByIdAsync(int id)
		{
			return await _context.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Book newBook)
		{
			await _context.UpdateAsync(newBook);
		}
	}
}
