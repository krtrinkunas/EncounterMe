using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class newDB6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "numberOfRatings",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numberOfRatings",
                table: "Locations");
        }
    }
}
