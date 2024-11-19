using Lab5BlazorApp.Components.Pages;
using Lab5BlazorApp.Models;
using Lab5BlazorApp.Services;
using System.Runtime.InteropServices;

namespace Lab5Tests {
	[TestClass]
	public class UnitTest1 {

		//
		//BOOK CHECKS
		//

		[TestMethod]
		public void TestReadBooksSuccess() {

			//Arrange + Act
			LibraryService lib = new LibraryService();

			//ReadBooks is called on init

			//Assert
			Assert.IsNotNull(lib.Books);

		}

		[DataTestMethod]
		[DataRow("The best book", "Moore", "0")]
		[DataRow("", "", "")]
		[DataRow(null, "1", "1")]
		public void TestAddBookSuccess(string title, string author, string isbn) {

			//Arrange
			LibraryService lib = new LibraryService();

			//Act
			lib.AddBook(title, author, isbn);

			//Assert
			Console.WriteLine(lib.Books.Last().Title);
			Assert.IsTrue(lib.Books.Exists(x => x.Title == title && x.Author == author && x.ISBN == isbn));

		}

		[TestMethod]
		public void TestDeleteBookSuccess() {

			//Arrange
			LibraryService lib = new LibraryService();

			//Act
			Book book = lib.Books.First();
			lib.DeleteBook(book);

			//Assert
			Assert.IsFalse(lib.Books.Exists(x => (x.Title == book.Title && x.Author == book.Author && x.ISBN == book.ISBN)));

		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestDeleteBookFail() {

			//Arrange
			LibraryService lib = new LibraryService();

			//Act
			Book book = new Book { Id = 20032, Author = "idkhi", Title = "Amazing book", ISBN = "1000000000" };
			lib.DeleteBook(book);

		}

		[TestMethod]
		public void TestEditBookSuccess() {

			//Arrange
			LibraryService lib = new LibraryService();

			//Act
			Book book = lib.Books.First();
			book.Title = "EDITED TITLE";
			lib.EditBook(book);

			//Assert
			Assert.IsTrue(lib.Books.Exists(x => (x.Title == book.Title && x.Author == book.Author && x.ISBN == book.ISBN)));

		}

		//
		//USER CHECKS
		//

		[TestMethod]
		public void TestReadUsersSuccess() {

			//Arrange + Act
			LibraryService lib = new LibraryService();

			//ReadUsers is called on init

			//Assert
			Assert.IsNotNull(lib.Users);


		}

		[DataTestMethod]
		[DataRow("hi there", "@gmail.comthingy")]
		[DataRow("", "")]
		[DataRow(null, "1")]
		public void TestAddUserSuccess(string name, string email) {

			//Arrange
			LibraryService lib = new LibraryService();

			//Act
			lib.AddUser(name, email);

			Console.WriteLine(lib.Users.Last().Name);
			Assert.IsTrue(lib.Users.Exists(x => x.Name == name && x.Email == email));

		}

		[TestMethod]
		public void TestDeleteUserSuccess() {

			//Arrange
			LibraryService lib = new LibraryService();

			//Act
			User user = lib.Users.First();
			lib.DeleteUser(user);

			//Assert
			Assert.IsFalse(lib.Users.Exists(x => (x.Name == user.Name && x.Email == user.Email)));

		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestDeleteUserFail() {

			//Arrange
			LibraryService lib = new LibraryService();

			//Act
			User user = new User { Id = 20032, Name = "hellomynameis", Email = "yes@gmal.com" };
			lib.DeleteUser(user);

		}

		[TestMethod]
		public void TestEditUserSuccess() {

			//Arrange
			LibraryService lib = new LibraryService();

			//Act
			User user = lib.Users.First();
			user.Name = "EDITED NAME";
			lib.EditUser(user);

			//Assert
			Assert.IsTrue(lib.Users.Exists(x => (x.Name == user.Name && x.Email == user.Email)));

		}
	}
}