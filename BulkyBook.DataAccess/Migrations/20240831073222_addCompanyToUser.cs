using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookWeb.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2024, 8, 31, 13, 2, 21, 168, DateTimeKind.Local).AddTicks(8756));

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2024, 8, 31, 13, 2, 21, 168, DateTimeKind.Local).AddTicks(8781));

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2024, 8, 31, 13, 2, 21, 168, DateTimeKind.Local).AddTicks(8782));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2024, 6, 8, 22, 27, 42, 133, DateTimeKind.Local).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2024, 6, 8, 22, 27, 42, 133, DateTimeKind.Local).AddTicks(5133));

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2024, 6, 8, 22, 27, 42, 133, DateTimeKind.Local).AddTicks(5134));
        }
    }
}
