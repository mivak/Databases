namespace ReadExcelFile
{
    using System;
    using System.Data.OleDb;

    public class ReadExcelFile
    {
        public static void Main()
        {
            OleDbConnection dbCon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=C:\TelerikAcademy\Databases\ADO.NET\AdoNet\ReadExcelFile\Book1.xlsx; Extended Properties=""Excel 12.0 XML;HDR=Yes""");
            OleDbCommand myCommand = new OleDbCommand("select * from [Sheet1$]", dbCon);
            dbCon.Open();
            OleDbDataReader reader = myCommand.ExecuteReader();

            using (dbCon)
            {
                while (reader.Read())
                {
                    string name = (string)reader["Name"];
                    double score = (double)reader["Score"];
                    Console.WriteLine("Name: {0} - score: {1}", name, score);
                }
            }
        }
    }
}