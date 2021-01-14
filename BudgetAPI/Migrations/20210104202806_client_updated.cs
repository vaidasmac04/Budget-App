using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetAPI.Migrations
{
    public partial class client_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Client",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Client",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Client",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Client",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Client",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
