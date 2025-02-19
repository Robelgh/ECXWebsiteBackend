using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tradeA2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "384256e2-b96e-44ed-bf63-b93ef68d0f5d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d673d345-ce71-4909-91f2-d76a954f08b5");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TradeAnalysises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bbd5f298-6208-4a01-b480-2f14b6cb096c", "5eb89381-5f09-4113-b25b-99c62b15694a", "Administrator", "ADMINISTRATOR" },
                    { "df2732dd-148e-42ce-bf2d-d5c4a34300e1", "9cd5be64-f586-4365-9854-ce4ca94c2bbe", "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "bbd5f298-6208-4a01-b480-2f14b6cb096c");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "df2732dd-148e-42ce-bf2d-d5c4a34300e1");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TradeAnalysises");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "384256e2-b96e-44ed-bf63-b93ef68d0f5d", "aa7e4348-a335-43f1-8181-573e29355b8d", "Administrator", "ADMINISTRATOR" },
                    { "d673d345-ce71-4909-91f2-d76a954f08b5", "832c7620-802f-4859-9e79-40bea6b726fa", "Visitor", "VISITOR" }
                });
        }
    }
}
