using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PocketApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PocketUser",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
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
                name: "PocketUserSettings",
                columns: table => new
                {
                    user_setting_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    avatar_url = table.Column<string>(type: "longtext", nullable: false),
                    theme = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    language = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    date_format = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocketUserSettings", x => x.user_setting_id);
                    table.ForeignKey(
                        name: "FK_PocketUserSettings_PocketUser_user_id",
                        column: x => x.user_id,
                        principalTable: "PocketUser",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PocketExpense_user_id",
                table: "PocketExpense",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_PocketUserSettings_user_id",
                table: "PocketUserSettings",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PocketExpense");

            migrationBuilder.DropTable(
                name: "PocketUserSettings");

            migrationBuilder.DropTable(
                name: "PocketUser");
        }
    }
}
