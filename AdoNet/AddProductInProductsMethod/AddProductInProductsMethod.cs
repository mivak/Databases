namespace AddProductInProductsMethod
{
    using System;
    using System.Data.SqlClient;

    public class AddProductInProductsMethod
    {
        public static void Main()
        {
            // Write a method that adds a new product in the products table in the Northwind database.
            // Use a parameterized SQL command.
            SqlConnection dbCon = new SqlConnection("Server=.\\SQLEXPRESS; Database=Northwind; Integrated Security=true;");

            var queryString = "INSERT INTO Products (ProductName, SupplierID, CategoryID, QuantityPerUnit," +
                            " UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) " +
                            "VALUES('Beer', 1, 1, '6 cans x 12 boxes', 8, 10, 5, 5, 0)";
            SqlCommand cmd = new SqlCommand(queryString, dbCon);
            AddProduct(cmd);
        }

        private static void AddProduct(SqlCommand cmd)
        {
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}