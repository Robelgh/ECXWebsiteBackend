using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations.ECXWebsiteAccountDb
{
    /// <inheritdoc />
    public partial class addAccouto1019071v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "5d2466e2-11a2-44ec-90bd-392c3e6c428c", "78397a8e-6046-40d9-af7f-9897e3cc1efe", "Administrator", "ADMINISTRATOR" },
                    { "746868bb-86e7-4314-b463-830147e6c7b4", "bda4c251-5372-4176-8f5f-96b6374160f3", "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d2466e2-11a2-44ec-90bd-392c3e6c428c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "746868bb-86e7-4314-b463-830147e6c7b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b945293-9463-4d48-a0aa-d2dbdde8be04", "9af035db-abed-4c55-a341-6c092e83084d", "Visitor", "VISITOR" },
                    { "b13c1c8e-12e9-4c94-9c1a-6bc2ce170810", "025490ee-0aae-400d-bc76-8c9193d4e6c8", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
