using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PracticeReactApp.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialApiMenuData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "APIEndpoint",
                columns: new[] { "Code", "IsActive", "Name", "Path" },
                values: new object[,]
                {
                    { "AC0001", true, "Account", "Account" },
                    { "RM0001", true, "Role mangement", "RoleManagement" },
                    { "TA0001", true, "Test Aware", "TestAware" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Code", "Attribute", "Icon", "IsActive", "IsDisplay", "Name", "Order", "ParentCode", "Path" },
                values: new object[,]
                {
                    { "RM0001", null, "ic_user", true, true, "Role", 2, null, "/role" },
                    { "US0001", null, "ic_user", true, true, "User", 1, null, "/user" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "000000",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5f870de6-5bfe-4e51-877f-25714f576468", "92b8c01b-1fd9-404c-8af0-9808032b985d" });

            migrationBuilder.InsertData(
                table: "RoleAPIEndpoint",
                columns: new[] { "ID", "APICode", "RoleID" },
                values: new object[,]
                {
                    { 1L, "AC0001", "ADMIN" },
                    { 2L, "RM0001", "ADMIN" },
                    { 3L, "TA0001", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "ID", "MenuCode", "RoleID" },
                values: new object[,]
                {
                    { 1L, "US0001", "ADMIN" },
                    { 2L, "RM0001", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleAPIEndpoint",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RoleAPIEndpoint",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "RoleAPIEndpoint",
                keyColumn: "ID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "APIEndpoint",
                keyColumn: "Code",
                keyValue: "AC0001");

            migrationBuilder.DeleteData(
                table: "APIEndpoint",
                keyColumn: "Code",
                keyValue: "RM0001");

            migrationBuilder.DeleteData(
                table: "APIEndpoint",
                keyColumn: "Code",
                keyValue: "TA0001");

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Code",
                keyValue: "RM0001");

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Code",
                keyValue: "US0001");

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "000000",
                columns: new[] { "ConcurrencyStamp", "NickName", "SecurityStamp" },
                values: new object[] { "a31e4c81-16af-4289-b3ad-06bc33d750a2", null, "600f3f56-31ca-425a-a960-2e534d574458" });
        }
    }
}
