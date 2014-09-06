namespace Atm.Data
{
    using System.Data.Entity;

    using Atm.Data.Migrations;
    using Atm.Model;
    
    public class AtmDbContext : DbContext
    {
        public AtmDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AtmDbContext, Configuration>());
        }

        public IDbSet<CardAccount> CardAccounts { get; set; }

        public IDbSet<TransactionsHistory> TransactionsHistories { get; set; }
    }
}