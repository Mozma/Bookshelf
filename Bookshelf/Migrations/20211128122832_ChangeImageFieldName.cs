using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshelf.Migrations
{
    public partial class ChangeImageFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Images_BookImageId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BookImageId",
                table: "Books",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_BookImageId",
                table: "Books",
                newName: "IX_Books_ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Images_ImageId",
                table: "Books",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Images_ImageId",
                table: "Books");


            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Books",
                newName: "BookImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_ImageId",
                table: "Books",
                newName: "IX_Books_BookImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Images_BookImageId",
                table: "Books",
                column: "BookImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
