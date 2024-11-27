namespace BookTest.Models
{
	public class SortViewModel
	{
		public SortBook AuthorSort { get; }
		public SortBook NameSort { get; }
		public SortBook DateSort { get; }
		public SortBook Current {  get; }

		public SortViewModel(SortBook sortOrder)
		{
			AuthorSort = sortOrder == SortBook.AuthorAsc ? SortBook.AuthorDesc : SortBook.AuthorAsc;
			NameSort = sortOrder == SortBook.NameAsc ? SortBook.NameDesc : SortBook.NameAsc;
			DateSort = sortOrder == SortBook.DateAsc ? SortBook.DateDesc : SortBook.DateAsc;
			Current = sortOrder;
		}
	}
}
