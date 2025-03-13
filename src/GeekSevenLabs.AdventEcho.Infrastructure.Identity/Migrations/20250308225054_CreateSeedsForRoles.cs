using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekSevenLabs.AdventEcho.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class CreateSeedsForRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bf8bd1f-0db0-4462-b114-0d0900125eb6", null, "Member", "MEMBER" },
                    { "5ed933c0-8472-4f02-a17c-3d9eca44ac20", null, "Elder", "ELDER" },
                    { "8b00570d-7b91-4412-8f31-d200171806f8", null, "Director", "DIRECTOR" },
                    { "b9a02c61-28ee-4b6e-acfb-fbd0b7d66639", null, "Developer", "DEVELOPER" },
                    { "c87aab47-2814-45d0-84e4-c824887e780e", null, "Administrator", "ADMINISTRATOR" },
                    { "e98609b7-2d43-4fca-b041-b50d619ac490", null, "Pastor", "PASTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bf8bd1f-0db0-4462-b114-0d0900125eb6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ed933c0-8472-4f02-a17c-3d9eca44ac20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b00570d-7b91-4412-8f31-d200171806f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9a02c61-28ee-4b6e-acfb-fbd0b7d66639");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c87aab47-2814-45d0-84e4-c824887e780e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e98609b7-2d43-4fca-b041-b50d619ac490");
        }
    }
}
