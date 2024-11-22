using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations.ECXWebsiteAccountDb
{
    /// <inheritdoc />
    public partial class account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "a0d5afb0-d92b-4f06-bd58-27ca5253e45c", "925da2d0-c3e6-414b-a172-7d75566001d6", "Visitor", "VISITOR" },
                    { "ff960189-9db6-478a-8931-00125eabe80a", "5ea053c8-1933-4fc7-a143-f7ccc6ca51c0", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0d5afb0-d92b-4f06-bd58-27ca5253e45c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff960189-9db6-478a-8931-00125eabe80a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d2466e2-11a2-44ec-90bd-392c3e6c428c", "78397a8e-6046-40d9-af7f-9897e3cc1efe", "Administrator", "ADMINISTRATOR" },
                    { "746868bb-86e7-4314-b463-830147e6c7b4", "bda4c251-5372-4176-8f5f-96b6374160f3", "Visitor", "VISITOR" }
                });
        }
    }
}
