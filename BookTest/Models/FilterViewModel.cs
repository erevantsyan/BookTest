namespace BookTest.Models
{
	public class FilterViewModel
	{
		public FilterViewModel(string author, string name, DateTime datePublic)
		{
			SelectedAuthor = author;
			SelectedName = name;
			SelectedDate = datePublic;
		}

		public string SelectedAuthor { get; }
		public string SelectedName { get; }
		public DateTime SelectedDate { get; }
	}
}
