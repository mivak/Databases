namespace Atm.ConsoleClient
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using Atm.Data;
    using Atm.Model;
    using RetrieveMoneyUsingTransactions;
    
    public class AtmConsoleClient
    {
        
        public static void Main()
        {
            var db = new AtmDbContext();
            //InsertSomeData(db);
            RetrieveMoneyUsingTransactions.RetrieveMoneyFromAccount("1234567251", "1251", 200, db);
                
            db.SaveChanges();

        }

        private static void InsertSomeData(AtmDbContext db)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (i < 10)
                {
                    db.CardAccounts.Add(new CardAccount
                    {
                        CardNumber = "123456789" + i.ToString(),
                        CardPin = "123" + i.ToString(),
                        CardCash = i
                    });
                }
                else if (i < 100)
                {
                    db.CardAccounts.Add(new CardAccount
                    {
                        CardNumber = "12345678" + i.ToString(),
                        CardPin = "12" + i.ToString(),
                        CardCash = i
                    });
                }
                else
                {
                    db.CardAccounts.Add(new CardAccount
                    {
                        CardNumber = "1234567" + i.ToString(),
                        CardPin = "1" + i.ToString(),
                        CardCash = i
                    });
                }
            }
        }
    }
}