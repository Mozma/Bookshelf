using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshelf.Migrations
{
    public partial class NewDataModelWithNewBindings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBinds_Shelves_ShelfId",
                table: "BookBinds");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Statuses_StatusId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Statuses_StatusId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Shelves_StatusId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_BookBinds_ShelfId",
                table: "BookBinds");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "BookBinds");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Books",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ISBN",
                table: "Books",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Books",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShelfBinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShelfId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelfBinds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShelfBinds_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShelfBinds_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelfBinds_BookId",
                table: "ShelfBinds",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelfBinds_ShelfId",
                table: "ShelfBinds",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Statuses_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Statuses_StatusId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "ShelfBinds");

            migrationBuilder.DropIndex(
                name: "IX_Books_PublisherId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Shelves",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShelfId",
                table: "BookBinds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_StatusId",
                table: "Shelves",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBinds_ShelfId",
                table: "BookBinds",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBinds_Shelves_ShelfId",
                table: "BookBinds",
                column: "ShelfId",
                principalTable: "Shelves",
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
                principalColumn: "Id");
        }
    }
}
