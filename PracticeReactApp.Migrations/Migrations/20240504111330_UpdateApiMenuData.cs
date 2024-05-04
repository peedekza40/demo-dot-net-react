using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeReactApp.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApiMenuData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "000000",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "eba95ca6-3849-4a9f-9ae4-5fa68a67bd62", "5f00788f-376b-40e0-882e-e3ff72664071" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "000000",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "08644677-d48b-4da4-887a-bf2d3d2373ba", "5e62e940-1916-4e9e-8159-9af20a9aedf7" });
        }
    }
}
