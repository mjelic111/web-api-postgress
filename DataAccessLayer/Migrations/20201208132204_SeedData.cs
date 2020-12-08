using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "BirthDate", "Name" },
                values: new object[] { 1, "Address 1", new DateTime(2019, 12, 8, 14, 22, 4, 367, DateTimeKind.Local).AddTicks(9970), "Test contact 1" });

            migrationBuilder.InsertData(
                table: "TelephoneNumber",
                columns: new[] { "Id", "ContactId", "Number" },
                values: new object[,]
                {
                    { 1, 1, "098444777" },
                    { 2, 1, "098555666" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TelephoneNumber",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TelephoneNumber",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
