using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("6ebb769f-3c8d-4513-a91b-a4ea32eaa0ba"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("bc46bd09-de5a-4190-a178-2b10149998d3"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("039732df-ed4a-4ac9-8b2a-e56de947f1a1"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8434eeac-ba08-42c7-8232-afe61ff20ac6"));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                schema: "Invoice",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "FullName", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "Phone", "UserId" },
                values: new object[,]
                {
                    { new Guid("2b2f796c-757f-4ed2-b4dd-02ddf83c868c"), "تهران، خیابان اول، پلاک ۱", null, null, null, null, null, "شرکت الفبا", null, null, null, "02112345678", new Guid("6712adb7-a20d-43e9-8b29-357271f3bd65") },
                    { new Guid("f7686f38-da84-4c96-9fd0-8219bdb2feee"), "تهران، خیابان اول، پلاک 2", null, null, null, null, null, "1شرکت الفبا", null, null, null, "02112345671", new Guid("92aa3814-ee96-4593-bdd3-cd613268137a") }
                });

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "ImagePath", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("401e8ea6-18ae-4aa9-9228-fa0339d27a45"), "PRD-20260514-A1B2C3", null, null, null, null, null, null, null, null, null, "لپ تاپ مدل X1", 55000000, 15 },
                    { new Guid("4c577160-916a-4033-913e-90245af8c9ba"), "PRD-20260514-D4E5F6", null, null, null, null, null, null, null, null, null, "کیبورد مکانیکی RGB", 2500000, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("2b2f796c-757f-4ed2-b4dd-02ddf83c868c"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("f7686f38-da84-4c96-9fd0-8219bdb2feee"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("401e8ea6-18ae-4aa9-9228-fa0339d27a45"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4c577160-916a-4033-913e-90245af8c9ba"));

            migrationBuilder.DropColumn(
                name: "ImagePath",
                schema: "Invoice",
                table: "Products");

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "FullName", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "Phone", "UserId" },
                values: new object[,]
                {
                    { new Guid("6ebb769f-3c8d-4513-a91b-a4ea32eaa0ba"), "تهران، خیابان اول، پلاک 2", null, null, null, null, null, "1شرکت الفبا", null, null, null, "02112345671", new Guid("92aa3814-ee96-4593-bdd3-cd613268137a") },
                    { new Guid("bc46bd09-de5a-4190-a178-2b10149998d3"), "تهران، خیابان اول، پلاک ۱", null, null, null, null, null, "شرکت الفبا", null, null, null, "02112345678", new Guid("6712adb7-a20d-43e9-8b29-357271f3bd65") }
                });

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("039732df-ed4a-4ac9-8b2a-e56de947f1a1"), "PRD-20260514-D4E5F6", null, null, null, null, null, null, null, null, "کیبورد مکانیکی RGB", 2500000, 50 },
                    { new Guid("8434eeac-ba08-42c7-8232-afe61ff20ac6"), "PRD-20260514-A1B2C3", null, null, null, null, null, null, null, null, "لپ تاپ مدل X1", 55000000, 15 }
                });
        }
    }
}
