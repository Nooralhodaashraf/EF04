using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF04.Models
{
    public class Transaction
    {
        public int TransactionNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; } = string.Empty;

        public string? Note { get; set; }

        // Foreign Key
        public string AccountNumber { get; set; } = string.Empty;

        // Navigation Property
        public Account Account { get; set; } = null!;
    }
}
