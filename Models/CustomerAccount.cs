using EF04.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF04.Models
{
    public class CustomerAccount
    {
        // Foreign Keys
        public int CustomerId { get; set; }

        public string AccountNumber { get; set; } = string.Empty;
        public AccountStatus AccountStatus { get; set; }

        // Relationship Attributes
        public OwnershipType OwnershipType { get; set; }

        public DateTime OwnershipStartDate { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; } = null!;

        public Account Account { get; set; } = null!;
    }
}
