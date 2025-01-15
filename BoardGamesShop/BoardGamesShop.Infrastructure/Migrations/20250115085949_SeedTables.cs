using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGamesShop.Infrastructure.Migrations
{
    public partial class SeedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2b8f34ed-f2d2-4ddf-8e2d-3f9647a360a1"), 0, "London Street Admin 3", "54396bff-b5d4-4780-bcd8-5a7f674f9f2e", "admin@mail.com", false, "Admin", false, "Adminov", false, null, null, null, "AQAAAAEAACcQAAAAEOYjAplbbnOCulhEMxa9zViiRMwl5pTfbCmAr3qxqpKulOcWAHeX8ETB/OiIleamMw==", "0888123456", false, null, false, "admin" },
                    { new Guid("2e448f34-5fe1-4b67-8b81-28ec0c20f7f3"), 0, "Paris Street Client 1", "2fec86be-36ba-422d-b761-369c8450aeae", "client@mail.com", false, "Client", false, "Clientov", false, null, null, null, "AQAAAAEAACcQAAAAEN5q8IgSaL3rSic9/c0CIMHCkEX/Ti3JUqEKVbL2kwpdwp4U26NyhuyViNZLAJoTjA==", "0999987654", false, null, false, "client" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Hasbro" },
                    { 2, "Space Cowboys" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Board Games" },
                    { 2, "Card Games" },
                    { 3, "Role-Playing Games" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2b8f34ed-f2d2-4ddf-8e2d-3f9647a360a1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2e448f34-5fe1-4b67-8b81-28ec0c20f7f3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
