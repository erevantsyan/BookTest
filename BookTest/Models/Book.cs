using System.ComponentModel.DataAnnotations;

namespace BookTest.Models
{
    public class Book
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Имя автора не заполнено")]
        public string Author { get; set; }
		[Required(ErrorMessage = "Название книги не заполнено")]
		public string Name { get; set; }
		public DateTime DatePublic { get; set; }
    }
}
