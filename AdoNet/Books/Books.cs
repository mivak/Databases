namespace SQLLiteBooks
{
    using System;
    using System.Text;
    using MySql.Data.MySqlClient;

    public class Books
    {
        static readonly MySqlConnection mySqlConn = new MySqlConnection(
                    @"Server=localhost;Database=dbo;Uid=root;pooling=true");

        public static void Main()
        {
            // Download and install MySQL database, MySQL Connector/Net (.NET Data Provider for MySQL) +
            // MySQL Workbench GUI administration tool . Create a MySQL database to store Books
            // (title, author, publish date and ISBN). Write methods for listing all books, finding a 
            // book by name and adding a book.
            CreateTable();
            AddBook("OOP", "Niki Kostov", "2013", "127-222-14", mySqlConn);
            Console.WriteLine("Please enter text to find a book");
            string text = Console.ReadLine();
            string escapedText = EscapeText(text);
            FindBook(escapedText, mySqlConn);
            ShowListOfAllBooks(mySqlConn);
        }

        private static string EscapeText(string text)
        {
            StringBuilder escapedText = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != '%' && text[i] != '-' && text[i] != '\'' && text[i] != ',' && text[i] != ';' &&
                    text[i] != ']' && text[i] != '[' && text[i] != '\\' && text[i] != '/' && text[i] != '_' &&
                    text[i] != '!')
                {
                    escapedText.Append(text[i]);
                }
            }

            return escapedText.ToString();
        }

        private static void CreateTable()
        {
            MySqlCommand create = new MySqlCommand(
                "CREATE TABLE books(title nvarchar(50), author nvarchar(50), publish_date datetime, ISBN nvarchar(50))",
                mySqlConn);
            
            mySqlConn.Open();
            using (mySqlConn)
            {
                create.ExecuteNonQuery();
            }
        }

        static void AddBook(string title, string author, string year, string isbn, MySqlConnection mySqlConnection)
        {
            MySqlCommand command = new MySqlCommand(
                "INSERT INTO books (title, author, publish_date, ISBN) VALUES (@title, @author, @year, @ISBN)",
                mySqlConnection);
            mySqlConnection.Open();
            using (mySqlConnection)
            {
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@ISBN", isbn);
                command.ExecuteNonQuery();
            }
        }

        static void FindBook(string bookTitle, MySqlConnection connection)
        {
            Console.WriteLine("\nFound books");
            MySqlCommand findCommand = new MySqlCommand
                ("SELECT author, title FROM books WHERE title like '%" + bookTitle + "%';", connection);
            connection.Open();
            using (connection)
            {
                var reader = findCommand.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string author = (string)reader["author"];
                        string title = (string)reader["title"];

                        Console.WriteLine(author + " " + title);
                    }
                }
            }
        }


        static void ShowListOfAllBooks(MySqlConnection connection)
        {
            Console.WriteLine("\nAll books:\n");
            MySqlCommand command = new MySqlCommand("SELECT title FROM Books", connection);
            connection.Open();
            var reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    string bookTitle = (string)reader["title"];
                    Console.WriteLine(bookTitle);
                }
            }
        }
    }
}