namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Data.Entity.Migrations;

    using StudentSystem.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }

        protected override void Seed(StudentSystem.Data.StudentSystemDbContext context)
        {
            context.Students.Add(new Student
            {
                Number = GenerateRandomNumber(100000, 999999).ToString(),
                Name = GenerateRandomString(10)
            });
        }

        private int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private string GenerateRandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char symbol;
            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    symbol = (char)((random.Next(1, 26) + 65));
                    builder.Append(symbol);
                }
                else
                {
                    symbol = (char)((random.Next(1, 26) + 97));
                    builder.Append(symbol);
                }
            }

            return builder.ToString();
        }
    }
}