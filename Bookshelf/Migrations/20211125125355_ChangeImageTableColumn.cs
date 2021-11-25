using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshelf.Migrations
{
    public partial class ChangeImageTableColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Binary",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Base64Data",
                table: "Images",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64Data",
                table: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "Binary",
                table: "Images",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
