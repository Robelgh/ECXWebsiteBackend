using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tradeA1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1c71431c-f4a3-4659-8913-f8bef1807d56");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9b37aec1-bb26-4d9b-94c7-7aaa16e54931");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "TradeAnalysises",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TradeAnalysises",
                newName: "Summary");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "384256e2-b96e-44ed-bf63-b93ef68d0f5d", "aa7e4348-a335-43f1-8181-573e29355b8d", "Administrator", "ADMINISTRATOR" },
                    { "d673d345-ce71-4909-91f2-d76a954f08b5", "832c7620-802f-4859-9e79-40bea6b726fa", "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "384256e2-b96e-44ed-bf63-b93ef68d0f5d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d673d345-ce71-4909-91f2-d76a954f08b5");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TradeAnalysises",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "TradeAnalysises",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c71431c-f4a3-4659-8913-f8bef1807d56", "51526ef4-8868-45fd-b254-e6a6f34c1807", "Administrator", "ADMINISTRATOR" },
                    { "9b37aec1-bb26-4d9b-94c7-7aaa16e54931", "a6b3ebc5-ac44-4f09-b064-a351fe437ec0", "Visitor", "VISITOR" }
                });
        }
    }
}
