using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SG.Migrations
{
    public partial class SolveRepetitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Risks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Risks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
