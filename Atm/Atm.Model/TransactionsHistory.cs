namespace Atm.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TransactionsHistory
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
    }
}