using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshelf.Migrations
{
    public partial class ApplyNewConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_AuthorImageId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Images_BookImageId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Statuses_StatusId",
                table: "Shelves");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Images",
                newName: "Binary");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Statuses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Shelves",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BookImageId",
                table: "Books",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorImageId",
                table: "Authors",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Authors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Images_AuthorImageId",
                table: "Authors",
                column: "AuthorImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Images_BookImageId",
                table: "Books",
                column: "BookImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Statuses_StatusId",
                table: "Shelves",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_AuthorImageId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Images_BookImageId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Statuses_StatusId",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Binary",
                table: "Images",
                newName: "Data");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Statuses",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Shelves",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookImageId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorImageId",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Images_AuthorImageId",
                table: "Authors",
                column: "AuthorImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Images_BookImageId",
                table: "Books",
                column: "BookImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Statuses_StatusId",
                table: "Shelves",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
