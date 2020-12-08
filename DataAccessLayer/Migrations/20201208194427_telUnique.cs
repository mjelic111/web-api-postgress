using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class telUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TelephoneNumber_ContactId",
                table: "TelephoneNumber");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(2019, 12, 8, 20, 44, 26, 453, DateTimeKind.Local).AddTicks(8774), new DateTime(2020, 12, 8, 20, 44, 26, 456, DateTimeKind.Local).AddTicks(8003) });

            migrationBuilder.UpdateData(
                table: "TelephoneNumber",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 8, 20, 44, 26, 457, DateTimeKind.Local).AddTicks(464));

            migrationBuilder.UpdateData(
                table: "TelephoneNumber",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 8, 20, 44, 26, 457, DateTimeKind.Local).AddTicks(508));

            migrationBuilder.CreateIndex(
                name: "IX_TelephoneNumber_ContactId_Number",
                table: "TelephoneNumber",
                columns: new[] { "ContactId", "Number" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TelephoneNumber_ContactId_Number",
                table: "TelephoneNumber");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(2019, 12, 8, 17, 14, 24, 507, DateTimeKind.Local).AddTicks(5605), new DateTime(2020, 12, 8, 17, 14, 24, 509, DateTimeKind.Local).AddTicks(9788) });

            migrationBuilder.UpdateData(
                table: "TelephoneNumber",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 8, 17, 14, 24, 510, DateTimeKind.Local).AddTicks(1514));

            migrationBuilder.UpdateData(
                table: "TelephoneNumber",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 8, 17, 14, 24, 510, DateTimeKind.Local).AddTicks(1811));

            migrationBuilder.CreateIndex(
                name: "IX_TelephoneNumber_ContactId",
                table: "TelephoneNumber",
                column: "ContactId");
        }
    }
}
