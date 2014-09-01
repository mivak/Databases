namespace AdoNet
{
    using System;
    using System.Data.SqlClient;

    public class RetrieveTheNumberOfCategories
    {
        public static void Main()
        {
            // Write a program that retrieves from the Northwind sample database in MS SQL Server
            // the number of  rows in the Categories table.
            SqlConnection dbCon = new SqlConnection("Server=.\\SQLEXPRESS; " +
            "Database=Northwind; Integrated Security=true");
            dbCon.Open();

            using(dbCon)
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Categories", dbCon);
                int categoriesCount = (int)cmd.ExecuteScalar();
                Console.WriteLine("The number of  rows in the Categories table is: {0}", categoriesCount);
            }
        }
    }
}