namespace RetrieveImages
{
    using System;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class RetrieveImages
    {
        public static void Main()
        {
            // Write a program that retrieves the images for all categories in the Northwind database
            // and stores them as JPG files in the file system.
            SqlConnection dbCon = new SqlConnection(
                "Server=.\\SQLEXPRESS; " +
                "Database=Northwind; " +
                "Integrated Security=true");

            dbCon.Open();
            using (dbCon)
            {
                SqlCommand command = new SqlCommand("SELECT CategoryName, Picture FROM Categories", dbCon);
                var reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        byte[] rawData = (byte[])reader["Picture"];
                        string fileName = reader["CategoryName"].ToString().Replace('/', '_') + ".jpg";
                        int len = rawData.Length;
                        int header = 78;
                        byte[] imgData = new byte[len - header];
                        Array.Copy(rawData, 78, imgData, 0, len - header);

                        MemoryStream memoryStream = new MemoryStream(imgData);
                        var image = Image.FromStream(memoryStream);
                        image.Save(new FileStream(fileName, FileMode.Create), ImageFormat.Jpeg);
                    }
                }
            }

            Console.WriteLine("Images saved successfully!");
        }
    }
}