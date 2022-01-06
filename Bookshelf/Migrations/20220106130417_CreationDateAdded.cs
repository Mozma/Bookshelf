using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Bookshelf.Migrations
{
    public partial class CreationDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Books");
        }
    }
}
