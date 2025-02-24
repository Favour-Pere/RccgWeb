using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RccgWeb.Migrations
{
    /// <inheritdoc />
    public partial class MadeJuctionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChurchId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChurchId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserChurches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChurchId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChurches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChurches_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChurches_UserId",
                table: "UserChurches",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChurches");

            migrationBuilder.AddColumn<string>(
                name: "ChurchId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChurchId",
                table: "AspNetUsers",
                column: "ChurchId",
                unique: true,
                filter: "[ChurchId] IS NOT NULL");
        }
    }
}
