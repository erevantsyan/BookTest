
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

		public async Task<IQueryable<Book>> GetAllAsync()
		{
			IQueryable<Book> result = _context.Books;
			return result;
		}

		public async Task<Book?> GetByIdAsync(int id)
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
