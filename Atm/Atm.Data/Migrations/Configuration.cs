namespace Atm.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Atm.Data.AtmDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "Atm.Data.AtmDbContext";
        }

        protected override void Seed(Atm.Data.AtmDbContext context)
        {
        }
    }
}
