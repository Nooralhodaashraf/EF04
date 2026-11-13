using EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF04.Configurations
{
    public class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            // Composite Primary Key
            builder.HasKey(ca => new
            {
                ca.CustomerId,
                ca.AccountNumber
            });

            // Customer Relationship
            builder.HasOne(ca => ca.Customer)
                   .WithMany(c => c.CustomerAccounts)
                   .HasForeignKey(ca => ca.CustomerId);

            // Account Relationship
            builder.HasOne(ca => ca.Account)
                   .WithMany(a => a.CustomerAccounts)
                   .HasForeignKey(ca => ca.AccountNumber);

            builder.Property(ca => ca.OwnershipType)
                   .IsRequired();

            builder.Property(ca => ca.OwnershipStartDate)
                   .IsRequired();

            builder.Property(ca => ca.AccountStatus)
                   .IsRequired();
        }
    }
}
