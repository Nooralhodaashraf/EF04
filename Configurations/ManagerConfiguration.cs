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
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.FullName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(m => m.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(m => m.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(m => m.BranchCode)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.HasIndex(m => m.BranchCode)
                   .IsUnique();


            builder.HasData(
                new Manager
                {
                    Id = 1,
                    FullName = "Ahmed Mohamed",
                    Email = "ahmed@bank.com",
                    PhoneNumber = "01012345678",
                    HireDate = new DateTime(2020, 1, 15),
                    BranchCode = "BR001"
                },
                new Manager
                {
                    Id = 2,
                    FullName = "Mona Ali",
                    Email = "mona@bank.com",
                    PhoneNumber = "01123456789",
                    HireDate = new DateTime(2021, 5, 10),
                    BranchCode = "BR002"
                }
            );

        }
    }
}
