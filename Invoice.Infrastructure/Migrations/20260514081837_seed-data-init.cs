using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeddatainit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Invoice",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("45628e48-e0d3-4f2d-9786-d416d5abcf72"), "PRD-20260514-A1B2C3", null, null, null, null, null, null, null, null, "لپ تاپ مدل X1", 55000000, 15 },
                    { new Guid("62ad2886-3a1a-467d-ad65-8a64bb14a911"), "PRD-20260514-D4E5F6", null, null, null, null, null, null, null, null, "کیبورد مکانیکی RGB", 2500000, 50 }
                });

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Users",
                columns: new[] { "Id", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "PasswordHash", "PasswordSalt", "Phone", "Username" },
                values: new object[,]
                {
                    { new Guid("6712adb7-a20d-43e9-8b29-357271f3bd65"), null, null, null, null, null, null, null, null, "3c830e2af2e5db02e2f467634499d3c807e7b0c1b09c247a478c893703999b0e", "6081de2f-df32-4e79-a844-772054b8fb32", "09121234567", "adminUser" },
                    { new Guid("92aa3814-ee96-4593-bdd3-cd613268137a"), null, null, null, null, null, null, null, null, "3c830e2af2e5db02e2f467634499d3c807e7b0c1b09c247a478c893703999b0e", "6081de2f-df32-4e79-a844-772054b8fb32", "09139876543", "adminUser2" }
                });

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "FullName", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "Phone", "UserId" },
                values: new object[,]
                {
                    { new Guid("875b80bd-9c47-4a50-9410-6043fe3f01a8"), "تهران، خیابان اول، پلاک ۱", null, null, null, null, null, "شرکت الفبا", null, null, null, "02112345678", new Guid("6712adb7-a20d-43e9-8b29-357271f3bd65") },
                    { new Guid("8c1bf588-f1ce-400d-97d5-ec413300c9b0"), "تهران، خیابان اول، پلاک 2", null, null, null, null, null, "1شرکت الفبا", null, null, null, "02112345671", new Guid("92aa3814-ee96-4593-bdd3-cd613268137a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("875b80bd-9c47-4a50-9410-6043fe3f01a8"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("8c1bf588-f1ce-400d-97d5-ec413300c9b0"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("45628e48-e0d3-4f2d-9786-d416d5abcf72"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("62ad2886-3a1a-467d-ad65-8a64bb14a911"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6712adb7-a20d-43e9-8b29-357271f3bd65"));

            migrationBuilder.DeleteData(
                schema: "Invoice",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("92aa3814-ee96-4593-bdd3-cd613268137a"));

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                schema: "Invoice",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
