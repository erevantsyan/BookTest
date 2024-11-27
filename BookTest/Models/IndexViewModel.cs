namespace BookTest.Models
{
	public class IndexViewModel
	{
		public IEnumerable<Book> Books { get; }
		public PageViewModel PageViewModel { get; }
		public FilterViewModel FilterViewModel { get; }
		public SortViewModel SortViewModel { get; }
		public IndexViewModel(IEnumerable<Book> books, PageViewModel pageViewModel, 
			FilterViewModel filterViewModel, SortViewModel sortViewModel)
		{
			Books = books;
			PageViewModel = pageViewModel;
			FilterViewModel = filterViewModel;
			SortViewModel = sortViewModel;
		}
	}
}
