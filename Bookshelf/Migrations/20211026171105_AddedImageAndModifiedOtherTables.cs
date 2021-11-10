using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshelf.Migrations
{
    public partial class AddedImageAndModifiedOtherTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Status_StatusId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Status_StatusId",
                table: "Shelves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Authors",
                newName: "AuthorImageId");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Books",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "PagesNumber",
                table: "Books",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "BookImageId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookImageId",
                table: "Books",
                column: "BookImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AuthorImageId",
                table: "Authors",
                column: "AuthorImageId");

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
                name: "FK_Books_Statuses_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "Statuses",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_AuthorImageId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Images_BookImageId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Statuses_StatusId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Statuses_StatusId",
                table: "Shelves");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookImageId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_AuthorImageId",
                table: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "BookImageId",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "AuthorImageId",
                table: "Authors",
                newName: "ImageId");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PagesNumber",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Status_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Status_StatusId",
                table: "Shelves",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
