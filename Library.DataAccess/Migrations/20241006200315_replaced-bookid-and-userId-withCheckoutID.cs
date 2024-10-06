using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class replacedbookidanduserIdwithCheckoutID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returns_AspNetUsers_AppUserId",
                table: "Returns");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Books_BookId",
                table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_Returns_AppUserId",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Returns");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Returns",
                newName: "CheckoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Returns_BookId",
                table: "Returns",
                newName: "IX_Returns_CheckoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Checkouts_CheckoutId",
                table: "Returns",
                column: "CheckoutId",
                principalTable: "Checkouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Checkouts_CheckoutId",
                table: "Returns");

            migrationBuilder.RenameColumn(
                name: "CheckoutId",
                table: "Returns",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Returns_CheckoutId",
                table: "Returns",
                newName: "IX_Returns_BookId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Returns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_AppUserId",
                table: "Returns",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_AspNetUsers_AppUserId",
                table: "Returns",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Books_BookId",
                table: "Returns",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
