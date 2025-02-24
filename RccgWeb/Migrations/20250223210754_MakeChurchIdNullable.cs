using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RccgWeb.Migrations
{
    /// <inheritdoc />
    public partial class MakeChurchIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChurchId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ChurchId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChurchId",
                table: "AspNetUsers",
                column: "ChurchId",
                unique: true,
                filter: "[ChurchId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChurchId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ChurchId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChurchId",
                table: "AspNetUsers",
                column: "ChurchId",
                unique: true);
        }
    }
}
