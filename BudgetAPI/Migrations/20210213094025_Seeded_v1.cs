using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetAPI.Migrations
{
    public partial class Seeded_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "food" },
                    { 2, "services" },
                    { 3, "clothing" },
                    { 4, "taxes" },
                    { 5, "healthcare" },
                    { 6, "household items" },
                    { 7, "gifts" },
                    { 8, "entertainment" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[,]
                {
                    { 1, "John", "Roy", new byte[] { 128, 236, 93, 244, 36, 33, 13, 12, 74, 200, 241, 199, 86, 53, 107, 238, 235, 61, 129, 50, 146, 174, 72, 249, 67, 198, 14, 61, 90, 32, 70, 35, 145, 109, 223, 254, 29, 113, 153, 42, 122, 63, 207, 95, 170, 188, 229, 196, 74, 187, 122, 245, 222, 173, 175, 96, 143, 35, 155, 251, 235, 248, 11, 76 }, new byte[] { 157, 192, 15, 176, 7, 21, 230, 170, 29, 51, 101, 100, 32, 197, 74, 108, 137, 58, 210, 90, 231, 108, 148, 103, 113, 56, 242, 21, 83, 100, 74, 87, 67, 212, 191, 63, 25, 174, 72, 44, 105, 130, 225, 143, 139, 248, 29, 94, 225, 18, 196, 177, 130, 70, 132, 155, 169, 171, 41, 3, 75, 43, 189, 163, 149, 48, 113, 90, 224, 63, 192, 190, 146, 223, 125, 109, 253, 60, 139, 222, 185, 81, 242, 91, 97, 6, 10, 79, 190, 53, 248, 249, 218, 36, 49, 121, 212, 208, 55, 103, 166, 37, 104, 160, 2, 243, 54, 251, 206, 108, 125, 15, 55, 13, 200, 250, 164, 69, 17, 73, 81, 82, 127, 162, 51, 135, 14, 15 }, "john123" },
                    { 2, "Oliver", "Jake", new byte[] { 146, 16, 173, 10, 171, 26, 50, 88, 71, 28, 19, 253, 100, 79, 194, 240, 135, 140, 182, 10, 110, 219, 134, 98, 17, 132, 86, 71, 18, 68, 124, 52, 145, 226, 126, 219, 245, 124, 238, 43, 223, 140, 76, 125, 155, 173, 62, 95, 44, 102, 116, 136, 60, 223, 184, 215, 131, 77, 128, 244, 181, 245, 85, 33 }, new byte[] { 142, 119, 21, 150, 0, 207, 87, 93, 54, 130, 228, 206, 221, 69, 60, 72, 52, 26, 169, 127, 129, 239, 168, 123, 42, 214, 132, 130, 95, 22, 238, 159, 123, 235, 118, 20, 126, 15, 237, 105, 175, 67, 138, 52, 245, 31, 121, 244, 20, 4, 138, 89, 245, 228, 91, 159, 8, 88, 226, 194, 199, 167, 8, 196, 91, 11, 231, 198, 104, 30, 134, 34, 164, 80, 41, 160, 136, 157, 32, 12, 166, 183, 37, 156, 113, 77, 23, 98, 107, 84, 76, 25, 87, 60, 169, 107, 11, 40, 188, 34, 121, 17, 125, 82, 27, 120, 224, 223, 12, 139, 192, 149, 55, 123, 15, 243, 57, 244, 114, 111, 223, 128, 204, 91, 125, 62, 228, 96 }, "oliver123" }
                });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "salary" },
                    { 2, "lottery" },
                    { 3, "sale" }
                });

            migrationBuilder.InsertData(
                table: "ClientCategories",
                columns: new[] { "ClientId", "CategoryId" },
                values: new object[,]
                {
                    { 2, 3 },
                    { 1, 1 },
                    { 1, 4 },
                    { 1, 7 },
                    { 2, 1 },
                    { 2, 5 },
                    { 2, 8 }
                });

            migrationBuilder.InsertData(
                table: "ClientSources",
                columns: new[] { "ClientId", "SourceId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 3 },
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Incomes",
                columns: new[] { "Id", "ClientId", "Date", "SourceId", "Value" },
                values: new object[,]
                {
                    { 3, 1, new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 25.140000000000001 },
                    { 2, 1, new DateTime(2020, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 100.86 },
                    { 5, 2, new DateTime(2020, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 710.86000000000001 },
                    { 4, 2, new DateTime(2021, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 850.0 },
                    { 1, 1, new DateTime(2021, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 500.0 },
                    { 6, 2, new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4100.0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 6, 8, "cinema" },
                    { 5, 3, "coat" },
                    { 4, 3, "sweater" },
                    { 3, 3, "jeans" },
                    { 2, 1, "oranges" },
                    { 7, 8, "theatre" },
                    { 1, 1, "ice cream" }
                });

            migrationBuilder.InsertData(
                table: "ClientItems",
                columns: new[] { "ClientId", "ItemId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 2, 5 },
                    { 1, 5 },
                    { 2, 6 },
                    { 2, 7 }
                });

            migrationBuilder.InsertData(
                table: "Outcomes",
                columns: new[] { "Id", "ClientId", "Date", "ItemId", "Price" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1.25 },
                    { 4, 1, new DateTime(2020, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 30.890000000000001 },
                    { 2, 2, new DateTime(2020, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 80.989999999999995 },
                    { 3, 2, new DateTime(2021, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 24.989999999999998 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ClientCategories",
                keyColumns: new[] { "ClientId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ClientCategories",
                keyColumns: new[] { "ClientId", "CategoryId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ClientCategories",
                keyColumns: new[] { "ClientId", "CategoryId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "ClientCategories",
                keyColumns: new[] { "ClientId", "CategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ClientCategories",
                keyColumns: new[] { "ClientId", "CategoryId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ClientCategories",
                keyColumns: new[] { "ClientId", "CategoryId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "ClientCategories",
                keyColumns: new[] { "ClientId", "CategoryId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "ClientItems",
                keyColumns: new[] { "ClientId", "ItemId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ClientItems",
                keyColumns: new[] { "ClientId", "ItemId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ClientItems",
                keyColumns: new[] { "ClientId", "ItemId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ClientItems",
                keyColumns: new[] { "ClientId", "ItemId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "ClientItems",
                keyColumns: new[] { "ClientId", "ItemId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "ClientItems",
                keyColumns: new[] { "ClientId", "ItemId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "ClientItems",
                keyColumns: new[] { "ClientId", "ItemId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "ClientItems",
                keyColumns: new[] { "ClientId", "ItemId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "ClientSources",
                keyColumns: new[] { "ClientId", "SourceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ClientSources",
                keyColumns: new[] { "ClientId", "SourceId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ClientSources",
                keyColumns: new[] { "ClientId", "SourceId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ClientSources",
                keyColumns: new[] { "ClientId", "SourceId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Outcomes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Outcomes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Outcomes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Outcomes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sources",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
