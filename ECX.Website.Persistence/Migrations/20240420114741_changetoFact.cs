using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changetoFact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "cab7b6a9-cc21-4a4d-a4fb-65e133f361d4");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e75d905b-a3bb-4838-8b13-1460b3c3274d");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Facts");

            migrationBuilder.DropColumn(
                name: "ImgName",
                table: "Facts");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Facts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f014cbb-96f9-478d-98dc-257cbe0edf09", "edc57adf-de86-42af-b09e-2e9a4cb547de", "Visitor", "VISITOR" },
                    { "e32a1fe0-a7bc-47f2-abef-1909aaf76350", "b353968f-fbd6-4889-a303-3ad386e6ca38", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4f014cbb-96f9-478d-98dc-257cbe0edf09");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e32a1fe0-a7bc-47f2-abef-1909aaf76350");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Facts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Facts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgName",
                table: "Facts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cab7b6a9-cc21-4a4d-a4fb-65e133f361d4", "a7e8ea9d-103a-4cdf-958d-019a4a98c53f", "Visitor", "VISITOR" },
                    { "e75d905b-a3bb-4838-8b13-1460b3c3274d", "9e9cf400-a031-498c-b9a3-993a71bf9175", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
