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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.AccountNumber);

            builder.Property(a => a.AccountNumber)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(a => a.CurrentBalance)
                   .HasPrecision(18, 2);

            builder.Property(a => a.AccountType)
                   .IsRequired();

            builder.Property(a => a.OpeningDate)
                   .IsRequired();

            builder.Property(a => a.BranchCode)
                   .HasMaxLength(10)
                   .IsRequired();

            // One-to-Many: Account - Transactions
            builder.HasMany(a => a.Transactions)
                   .WithOne(t => t.Account)
                   .HasForeignKey(t => t.AccountNumber)
                   .IsRequired();
        }
    }
}
