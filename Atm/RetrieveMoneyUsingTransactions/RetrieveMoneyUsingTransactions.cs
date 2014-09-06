using System;
using System.Linq;
using System.Transactions;
using Atm.Data;
using Atm.Model;

namespace RetrieveMoneyUsingTransactions
{
    public class RetrieveMoneyUsingTransactions
    {

        private static bool CheckIfCardNumberIsValid(string cardNumber, AtmDbContext db)
        {
            if (cardNumber == null)
            {
                throw new ArgumentNullException("Card number cannot be null");
            }

            string number = db.CardAccounts.Where(c => c.CardNumber == cardNumber).FirstOrDefault().CardNumber;

            return number == cardNumber;
        }

        private static bool CheckIfCardPinIsValid(string cardPin, AtmDbContext db)
        {
            if (cardPin == null)
            {
                throw new ArgumentNullException("Card pin cannot be null");
            }

            string pin = db.CardAccounts.Where(c => c.CardPin == cardPin).FirstOrDefault().CardPin;

            return pin == cardPin;
        }

        private static bool CheckIfAmountIsBiggerThanRequestedSum(string cardNumber, int requestedSum, AtmDbContext db)
        {
            if (cardNumber == null)
            {
                throw new ArgumentNullException("Card number or requested sum cannot be null");
            }

            decimal cash = (decimal)db.CardAccounts.Where(c => c.CardNumber == cardNumber).FirstOrDefault().CardCash;
            
            return cash >= requestedSum;
        }

        public static void RetrieveMoneyFromAccount(string cardNumber, string cardPin, int requestedSum, AtmDbContext db)
        {
            var options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.RepeatableRead;
            using (var scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using(db = new AtmDbContext())
                {
                    if (!CheckIfCardNumberIsValid(cardNumber, db))
                    {
                        scope.Dispose();
                        throw new ArgumentException("Card number is not valid!");
                    }

                    if (!CheckIfCardPinIsValid(cardPin, db))
                    {
                        scope.Dispose();
                        throw new ArgumentException("Card pin is not valid!");
                    }

                    if (!CheckIfAmountIsBiggerThanRequestedSum(cardNumber, 200, db))
                    {
                        scope.Dispose();
                        throw new ArgumentException("The amount in the account is smaller than the requested sum");
                    }

                    var cash = (decimal)db.CardAccounts.Where(c => c.CardNumber == cardNumber).FirstOrDefault().CardCash;
                    db.CardAccounts.Where(c => c.CardNumber == cardNumber).FirstOrDefault().CardCash = cash - requestedSum;
                    db.SaveChanges();

                    SaveTransactionHistory(cardNumber, cash - requestedSum, db);
                }

                scope.Complete();
            }
        }

        private static void SaveTransactionHistory(string cardNumber, decimal cash, AtmDbContext db)
        {
            var options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.RepeatableRead
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                db.TransactionsHistories.Add(new TransactionsHistory
                {
                    CardNumber = cardNumber,
                    TransactionDate = DateTime.Now,
                    Amount = cash
                });

                db.SaveChanges();
                scope.Complete();
            }
        }
    } 
}