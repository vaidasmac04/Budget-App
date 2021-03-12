
using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget.Persistence.Migrations
{
    public partial class outcome_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Outcomes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Outcomes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
