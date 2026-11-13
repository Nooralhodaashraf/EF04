using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF04.Migrations
{
    /// <inheritdoc />
    public partial class SeedBranchesAndManagers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Code", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "BR001", "Nasr City, Cairo", "Cairo Main Branch", "0223456789" },
                    { "BR002", "Smouha, Alexandria", "Alexandria Branch", "0334567890" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "BranchCode", "Email", "FullName", "HireDate", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "BR001", "ahmed@bank.com", "Ahmed Mohamed", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "01012345678" },
                    { 2, "BR002", "mona@bank.com", "Mona Ali", new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "01123456789" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "BR001");

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "BR002");
        }
    }
}
