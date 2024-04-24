using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PracticeReactApp.Server.Migrations.PracticeDotnetReact
{
    /// <inheritdoc />
    public partial class AddApiPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APIEndpoint",
                columns: table => new
                {
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Path = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIEndpoint", x => x.Code);
                });

            migrationBuilder.CreateTable(
                    name: "RoleAPIEndpoint",
                    columns: table => new
                    {
                        ID = table.Column<long>(type: "bigint", nullable: false)
                                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        RoleID = table.Column<string>(type: "text", nullable: false),
                        APICode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_RoleAPIEndpoint", x => x.ID);
                        table.ForeignKey(
                            name: "FK_RoleAPIEndpoint_Role_RoleId",
                            column: x => x.RoleID,
                            principalTable: "Role",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_RoleAPIEndpoint_APIEndpoint_APICode",
                            column: x => x.APICode,
                            principalTable: "APIEndpoint",
                            principalColumn: "Code",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateIndex(
                name: "APIEndpointIndex",
                table: "APIEndpoint",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleAPIEndpointIndex",
                table: "RoleAPIEndpoint",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAPIEndpoint");

            migrationBuilder.DropTable(
                name: "APIEndpoint");
        }
    }
}
