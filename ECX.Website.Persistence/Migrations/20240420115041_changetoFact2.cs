using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changetoFact2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4f014cbb-96f9-478d-98dc-257cbe0edf09");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e32a1fe0-a7bc-47f2-abef-1909aaf76350");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06bf76fc-617a-41d6-a3a9-7f66c27d9af4", "e6cccc20-f1de-4b88-a4ec-eb459cfcc6aa", "Visitor", "VISITOR" },
                    { "501f6eeb-e2da-4a87-b811-ef8fce25241b", "0431a1e5-1547-4856-bebd-648bc4ce4100", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "06bf76fc-617a-41d6-a3a9-7f66c27d9af4");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "501f6eeb-e2da-4a87-b811-ef8fce25241b");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f014cbb-96f9-478d-98dc-257cbe0edf09", "edc57adf-de86-42af-b09e-2e9a4cb547de", "Visitor", "VISITOR" },
                    { "e32a1fe0-a7bc-47f2-abef-1909aaf76350", "b353968f-fbd6-4889-a303-3ad386e6ca38", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
