namespace BookTest.Models
{
	public interface IBookRepository
	{
		Task<IQueryable<Book>> GetAllAsync();
		Task<Book?> GetByIdAsync(int id);
		Task AddAsync(Book book);
		Task UpdateAsync(Book newBook);
		Task DeleteAsync(Book book);
		Task<int> CountAsync();
	}
}
