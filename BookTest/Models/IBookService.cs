namespace BookTest.Models
{
	public interface IBookService
	{
		Task<IndexViewModel> GetAllAsync(string author, string name,
			DateTime datePublic, int page,
			SortBook sortOrder);
		Task<Book?> GetByIdAsync(int? id);
		Task AddAsync(Book book);
		Task UpdateAsync(Book newBook);
		Task DeleteAsync(Book book);
	}
}
