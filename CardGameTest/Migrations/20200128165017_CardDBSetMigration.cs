using Microsoft.EntityFrameworkCore.Migrations;

namespace CardGameTest.Migrations
{
    public partial class CardDBSetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    ID = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    DiceNeeded = table.Column<int>(nullable: false),
                    Desc = table.Column<string>(nullable: true),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
