namespace RetrieveTheNameAndDescriptionOfCategories
{
    using System;
    using System.Data.SqlClient;

    public class RetrieveTheNameAndDescriptionOfCategories
    {
        public static void Main()
        {
            // Write a program that retrieves the name and description of all
            // categories in the Northwind DB.
            SqlConnection dbcon = new SqlConnection("Server=.\\SQLEXPRESS; Database=Northwind; Integrated Security=true");
            dbcon.Open();
            using(dbcon)
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryName, Description FROM Categories", dbcon);
                SqlDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string categoryName = (string)reader["CategoryName"];
                        string categoryDescription = (string)reader["Description"];
                        Console.WriteLine("CatName: {0}; CatDescription: {1}", categoryName, categoryDescription);
                    }
                }
            }
        }
    }
}