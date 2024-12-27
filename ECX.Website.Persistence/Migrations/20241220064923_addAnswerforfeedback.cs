using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECX.Website.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAnswerforfeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0eb72e0b-c9c8-4c7c-bd5d-01de065535b6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a2979d18-eb1c-4b72-9980-dcd5edfc4b18");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "FeedBacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnsweredBy",
                table: "FeedBacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "answerSeen",
                table: "FeedBacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "requestSeen",
                table: "FeedBacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f2f92a2-c673-4635-a499-1d2dbf33df2b", "218ee57f-89ac-439a-8585-107e2c9a8381", "Visitor", "VISITOR" },
                    { "b729e394-a586-468f-9a62-0caa6614d95f", "00e9935a-e139-4164-847e-d6160faa738e", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3f2f92a2-c673-4635-a499-1d2dbf33df2b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b729e394-a586-468f-9a62-0caa6614d95f");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "FeedBacks");

            migrationBuilder.DropColumn(
                name: "AnsweredBy",
                table: "FeedBacks");

            migrationBuilder.DropColumn(
                name: "answerSeen",
                table: "FeedBacks");

            migrationBuilder.DropColumn(
                name: "requestSeen",
                table: "FeedBacks");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0eb72e0b-c9c8-4c7c-bd5d-01de065535b6", "0ef0aa8e-ab31-4a4c-b342-97d6677ace7a", "Visitor", "VISITOR" },
                    { "a2979d18-eb1c-4b72-9980-dcd5edfc4b18", "e85ff779-06d0-4ce1-8f6a-97cf3bf124c8", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
