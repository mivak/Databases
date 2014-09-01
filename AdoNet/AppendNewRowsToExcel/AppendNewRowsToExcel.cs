namespace AppendNewRowsToExcel
{
    using System;
    using System.Data.OleDb;

    public class AppendNewRowsToExcel
    {
        public static void Main()
        {
            // Implement appending new rows to the Excel file.
            OleDbConnection dbCon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=C:\TelerikAcademy\Databases\ADO.NET\AdoNet\AppendNewRowsToExcel\Book1.xlsx; Extended Properties=""Excel 12.0 XML;HDR=Yes""");

            InsertDataIntoEcxel("Hristov", 2, dbCon);
        }

        static void InsertDataIntoEcxel(string name, double score, OleDbConnection connection)
        {
            OleDbCommand myCommand = new OleDbCommand("INSERT INTO [Sheet1$] ([Name], [Score]) VALUES (@Name,@Score)", connection);
            connection.Open();
            using (connection)
            {
                myCommand.Parameters.AddWithValue("@Name", name);
                myCommand.Parameters.AddWithValue("@Score", score);
                myCommand.ExecuteNonQuery();
            }
        }
    }
}