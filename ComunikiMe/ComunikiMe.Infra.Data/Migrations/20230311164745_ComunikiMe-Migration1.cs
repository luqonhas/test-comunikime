using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComunikiMe.Infra.Data.Migrations
{
    public partial class ComunikiMeMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL", nullable: false, defaultValue: 0m),
                    Stock = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    Image = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false, defaultValue: "https://firebasestorage.googleapis.com/v0/b/podtv-5700.appspot.com/o/upload-image-icon.jpg?alt=media&token=129b5561-51a8-4c2f-81f0-264dcd2397d1"),
                    ModifyDate = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    InsertDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(6)", maxLength: 6, nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsers = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProducts = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Products_IdProducts",
                        column: x => x.IdProducts,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_Users_IdUsers",
                        column: x => x.IdUsers,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_IdProducts",
                table: "Carts",
                column: "IdProducts");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_IdUsers",
                table: "Carts",
                column: "IdUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
