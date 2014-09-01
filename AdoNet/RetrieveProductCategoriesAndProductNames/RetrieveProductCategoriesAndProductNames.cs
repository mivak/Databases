namespace RetrieveProductCategoriesAndProductNames
{
    using System;
    using System.Data.SqlClient;

    public class RetrieveProductCategoriesAndProductNames
    {
        public static void Main()
        {
            // Write a program that retrieves from the Northwind database all product categories and
            // the names of the products in each category. Can you do this with a single
            // SQL query (with table join)?
            SqlConnection dbCon = new SqlConnection("Server=.\\SQLEXPRESS; Database=Northwind; Integrated Security=true;");
            dbCon.Open();
            using (dbCon)
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT c.CategoryName, p.ProductName FROM Products p JOIN Categories c" + 
                    " ON c.CategoryID = p.CategoryID ORDER BY c.CategoryName",
                    dbCon);
                SqlDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string categoryName = (string)reader["CategoryName"];
                        string productName = (string)reader["ProductName"];
                        Console.WriteLine("Category: {0}; Product: {1}", categoryName, productName);
                    }
                }
            }
        }
    }
}