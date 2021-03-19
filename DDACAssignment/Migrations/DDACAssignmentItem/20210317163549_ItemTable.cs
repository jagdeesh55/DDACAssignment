using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDACAssignment.Migrations.DDACAssignmentItem
{
    public partial class ItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    ItemEntered = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
