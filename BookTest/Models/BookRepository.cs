
using Microsoft.EntityFrameworkCore;
using System;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookTest.Models
{
	public class BookRepository : IBookRepository
	{
		private readonly ApplicationContext _context;

		public BookRepository(ApplicationContext context)
		{
			_context = context;
		}
		public async Task AddAsync(Book book)
		{
			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Book book)
		{
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Book>> GetAllAsync(string author, 
			string name, DateTime datePublic, SortBook sortOrder,
			int page, int pageSize)
		{
			IQueryable<Book> books = _context.Books;

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

			return await books
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<Book?> GetByIdAsync(int? id)
		{
			var result = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
			return result;
		}

		public async Task UpdateAsync(Book newBook)
		{
			_context.Update(newBook);
			await _context.SaveChangesAsync();
		}

		public async Task<int> CountAsync()
		{
			IQueryable<Book> books = _context.Books;
			return await books.CountAsync();
		}
	}
}
