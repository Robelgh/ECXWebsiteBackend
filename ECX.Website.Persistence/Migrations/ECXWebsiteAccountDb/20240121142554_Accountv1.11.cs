using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations.ECXWebsiteAccountDb
{
    /// <inheritdoc />
    public partial class Accountv111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b05a8c29-47e5-40d2-9520-a6404d53ebb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf4b6672-4d23-4b86-b3fa-c1451ada5184");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b945293-9463-4d48-a0aa-d2dbdde8be04", "9af035db-abed-4c55-a341-6c092e83084d", "Visitor", "VISITOR" },
                    { "b13c1c8e-12e9-4c94-9c1a-6bc2ce170810", "025490ee-0aae-400d-bc76-8c9193d4e6c8", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b945293-9463-4d48-a0aa-d2dbdde8be04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b13c1c8e-12e9-4c94-9c1a-6bc2ce170810");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b05a8c29-47e5-40d2-9520-a6404d53ebb8", "57c0c19a-352f-4727-b633-305aafc845dc", "Visitor", "VISITOR" },
                    { "bf4b6672-4d23-4b86-b3fa-c1451ada5184", "de00c979-0c74-4515-84f5-82f77b4f7c11", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
