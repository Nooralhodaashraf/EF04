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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionNumber);

            builder.Property(t => t.TransactionNumber)
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(t => t.TransactionType)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(t => t.Note)
                   .HasMaxLength(500);

            builder.Property(t => t.AccountNumber)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(t => t.TransactionDate)
                   .IsRequired();
        }
    }
}
