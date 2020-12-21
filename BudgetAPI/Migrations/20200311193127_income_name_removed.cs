using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetAPI.Migrations
{
    public partial class income_name_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Income");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Income",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
