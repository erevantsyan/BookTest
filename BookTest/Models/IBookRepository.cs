namespace BookTest.Models
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAllAsync(string author,
			string name, DateTime datePublic, SortBook sortOrder,
			int page, int pageSize);
		Task<Book?> GetByIdAsync(int? id);
		Task AddAsync(Book book);
		Task UpdateAsync(Book newBook);
		Task DeleteAsync(Book book);
		Task<int> CountAsync();
	}
}
