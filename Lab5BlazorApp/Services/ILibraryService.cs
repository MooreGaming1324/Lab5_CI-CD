using Lab5BlazorApp.Models;

namespace Lab5BlazorApp.Services {
    public interface ILibraryService {

        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }
        public Dictionary<User, List<Book>> BorrowedBooks { get; set; }
        public void ReadBooks();
        public void ReadUsers();
        public void WriteBooks();
        public void WriteUsers();

        public void AddBook(string title, string author, string isbn);
        public void EditBook(Book book);
        public void DeleteBook(Book book);

        public void AddUser(string name, string email);
        public void EditUser(User user);
        public void DeleteUser(User user);

        public void BorrowBook(Book book, User user);
		public void ReturnBook(Book book, User user);


	}
}
