using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tradeA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "612d6dae-7fe8-4c0c-a12b-3a26b7e13d99");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "effa6401-d17c-4f2a-91f3-5727e9324334");

            migrationBuilder.CreateTable(
                name: "TradeAnalysises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeAnalysises", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c71431c-f4a3-4659-8913-f8bef1807d56", "51526ef4-8868-45fd-b254-e6a6f34c1807", "Administrator", "ADMINISTRATOR" },
                    { "9b37aec1-bb26-4d9b-94c7-7aaa16e54931", "a6b3ebc5-ac44-4f09-b064-a351fe437ec0", "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeAnalysises");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1c71431c-f4a3-4659-8913-f8bef1807d56");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9b37aec1-bb26-4d9b-94c7-7aaa16e54931");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "612d6dae-7fe8-4c0c-a12b-3a26b7e13d99", "92fa9a56-998b-48e0-a16e-794e9e2dcac3", "Visitor", "VISITOR" },
                    { "effa6401-d17c-4f2a-91f3-5727e9324334", "ecc1cdf7-429c-4563-8d75-cd7ea93eeefd", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
