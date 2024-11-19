using Lab5BlazorApp.Components.Pages;
using Lab5BlazorApp.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net;
using Microsoft.VisualBasic.FileIO;
using static System.Net.Mime.MediaTypeNames;
using System.Text;


namespace Lab5BlazorApp.Services {
    public class LibraryService : ILibraryService {

		public List<Book> Books { get; set; } = new List<Book>();
        public List<User> Users { get; set; } = new List<User>();
        public Dictionary<User, List<Book>> BorrowedBooks { get; set; } = new Dictionary<User, List<Book>>();

        public LibraryService() { 
            ReadBooks();
            ReadUsers();
        }
        public void ReadBooks() {
			try {
				using TextFieldParser parser = new TextFieldParser("Data/Books.csv");
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				parser.HasFieldsEnclosedInQuotes = true;

				while (!parser.EndOfData) {
					var fields = parser.ReadFields();
					if (fields.Length >= 4) {
						var book = new Book {
							Id = int.Parse(fields[0].Trim()),
							Title = fields[1].Trim(),
							Author = fields[2].Trim(),
							ISBN = fields[3].Trim()
						};

						Books.Add(book);
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}

        public void ReadUsers() {
			try {
				using TextFieldParser parser = new TextFieldParser("Data/Users.csv");
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				parser.HasFieldsEnclosedInQuotes = true;

				while (!parser.EndOfData) {
					var fields = parser.ReadFields();
					if (fields.Length >= 3) {
						var user = new User {
							Id = int.Parse(fields[0].Trim()),
							Name = fields[1].Trim(),
							Email = fields[2].Trim()
                        };

						Users.Add(user);
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
        }
		public void WriteBooks() {
			var csv = new StringBuilder();
			foreach (var book in Books) {
				var newLine = string.Format($"\"{book.Id}\",\"{book.Title}\",\"{book.Author}\",\"{book.ISBN}\"");
				csv.AppendLine(newLine);
			}
			File.WriteAllText("Data/Books.csv", csv.ToString());
		}

		public void WriteUsers() {
			var csv = new StringBuilder();
			foreach (var user in Users) {
				var newLine = string.Format($"\"{user.Id}\",\"{user.Name}\",\"{user.Email}\"");
				csv.AppendLine(newLine);
			}
			File.WriteAllText("Data/Users.csv", csv.ToString());
		}


        public void AddBook(string title, string author, string isbn) {
            int id = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
            Books.Add(new Book { Id = id, Title = title, Author = author, ISBN = isbn });
			WriteBooks();
		}

        public void AddUser(string name, string email) {
            int id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
            Users.Add(new User { Id = id, Name = name, Email = email });
			WriteUsers();
        }

        public void DeleteBook(Book book) {
			if (Books.Remove(book) == false) {
				throw new Exception();
			}
			WriteBooks();
		}

        public void DeleteUser(User user) {
			if (Users.Remove(user) == false) {
				throw new Exception();
			}
			WriteUsers();
		}

        public void EditBook(Book book) {
            Books[Books.FindIndex(b => b.Id == book.Id)] = book;
			WriteBooks();
		}

        public void EditUser(User user) {
            Users[Users.FindIndex(b => b.Id == user.Id)] = user;
			WriteUsers();
		}

        public void BorrowBook(Book book, User user) {
            if (!BorrowedBooks.ContainsKey(user)) {
                BorrowedBooks[user] = new List<Book>();
            }
            BorrowedBooks[user].Add(book);
            Books.Remove(book);
        }

		public void ReturnBook(Book book, User user) {
            BorrowedBooks[user].Remove(book);
            try {
				Books.Insert(book.Id, book);
			} catch {
                Books.Add(book);
            }

		}

	}
}
