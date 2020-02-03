using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGameTest.Migrations
{
    public partial class CardTableDropped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Desc = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    DiceNeeded = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Used = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.ID);
                });
        }
    }
}
