using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF04.Models
{
    public class Branch
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        // Navigation Properties
        public Manager Manager { get; set; } = null!;

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
