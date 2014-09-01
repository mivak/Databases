namespace FindByString
{
    using System;
    using System.Data.SqlClient;
    using System.Text;

    public class FindByString
    {
        public static void Main()
        {
            // Write a program that reads a string from the console and finds all products that contain
            // this string. Ensure you handle correctly characters like ', %, ", \ and _.

            Console.WriteLine("Please enter a string to find products: ");
            string text = Console.ReadLine();
            string escapedText = EscapeText(text);

            SqlConnection dbCon = new SqlConnection("Server=.\\SQLEXPRESS; " +
            "Database=Northwind; Integrated Security=true");

            dbCon.Open();
            using (dbCon)
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT ProductName FROM Products WHERE ProductName LIKE '%" + escapedText + "%'", dbCon);
                SqlDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string productName = (string)reader["ProductName"];
                        Console.WriteLine(productName);
                    }
                }
            }
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
    }
}