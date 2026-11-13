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
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Code);

            builder.Property(b => b.Code)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(b => b.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(b => b.Address)
                   .HasMaxLength(200);

            builder.Property(b => b.PhoneNumber)
                   .HasMaxLength(20);

            // One-to-One: Branch - Manager
            builder.HasOne(b => b.Manager)
                   .WithOne(m => m.Branch)
                   .HasForeignKey<Manager>(m => m.BranchCode)
                   .IsRequired();

            // One-to-Many: Branch - Accounts
            builder.HasMany(b => b.Accounts)
                   .WithOne(a => a.Branch)
                   .HasForeignKey(a => a.BranchCode)
                   .IsRequired();


            builder.HasData(
                new Branch
                {
                    Code = "BR001",
                    Name = "Cairo Main Branch",
                    Address = "Nasr City, Cairo",
                    PhoneNumber = "0223456789"
                },
                new Branch
                {
                    Code = "BR002",
                    Name = "Alexandria Branch",
                    Address = "Smouha, Alexandria",
                    PhoneNumber = "0334567890"
                }
            );

        }



    }
}
