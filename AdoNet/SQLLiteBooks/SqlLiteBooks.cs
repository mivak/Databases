namespace SQLLiteBooks
{
    using System;
    using System.Text;
    using System.Data.SQLite;
    public class SQLLiteBooks
    {
        static readonly SQLiteConnection sqLitecon = new SQLiteConnection(
                    @"Data Source=..\..\Books.db;Version=3;");

        public static void Main()
        {
            // Re-implement the previous task with SQLite embedded DB (see http://sqlite.phxsoftware.com).
            CreateTable();
            AddBook("OOP", "Niki Kostov", "2013", "127-222-14", sqLitecon);
            Console.WriteLine("Please enter text to find a book");
            string text = Console.ReadLine();
            string escapedText = EscapeText(text);
            FindBook(escapedText, sqLitecon);
            ShowListOfAllBooks(sqLitecon);
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
            SQLiteCommand create = new SQLiteCommand(
                "CREATE TABLE books(title nvarchar(50), author nvarchar(50), publish_date datetime, ISBN nvarchar(50))",
                sqLitecon);

            sqLitecon.Open();
            using (sqLitecon)
            {
                create.ExecuteNonQuery();
            }
        }

        static void AddBook(string title, string author, string year, string isbn, SQLiteConnection mySqlConnection)
        {
            SQLiteCommand command = new SQLiteCommand(
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

        static void FindBook(string bookTitle, SQLiteConnection connection)
        {
            Console.WriteLine("\nFound books");
            SQLiteCommand findCommand = new SQLiteCommand
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

        static void ShowListOfAllBooks(SQLiteConnection connection)
        {
            Console.WriteLine("\nAll books:\n");
            SQLiteCommand command = new SQLiteCommand("SELECT title FROM Books", connection);
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