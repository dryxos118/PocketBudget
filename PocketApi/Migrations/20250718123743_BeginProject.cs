using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PocketApi.Migrations
{
    /// <inheritdoc />
    public partial class BeginProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PocketRole",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    role_description = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocketRole", x => x.role_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PocketUser",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocketUser", x => x.user_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PocketExpense",
                columns: table => new
                {
                    expense_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    expense_type = table.Column<int>(type: "int", nullable: false),
                    expense_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    expense_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    expense_amount = table.Column<double>(type: "double", nullable: false),
                    expense_category = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocketExpense", x => x.expense_id);
                    table.ForeignKey(
                        name: "FK_PocketExpense_PocketUser_user_id",
                        column: x => x.user_id,
                        principalTable: "PocketUser",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RolesRoleId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RolesRoleId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserRoles_PocketRole_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "PocketRole",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_PocketUser_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "PocketUser",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "PocketRole",
                columns: new[] { "role_id", "role_description", "role_name" },
                values: new object[,]
                {
                    { 1, "", "PocketUser" },
                    { 2, "", "PocketAdmin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PocketExpense_user_id",
                table: "PocketExpense",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersUserId",
                table: "UserRoles",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PocketExpense");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "PocketRole");

            migrationBuilder.DropTable(
                name: "PocketUser");
        }
    }
}
