using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addrefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                schema: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DeletedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedByIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Invoice",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_RefreshToken",
                schema: "Invoice",
                table: "UserRefreshTokens",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                schema: "Invoice",
                table: "UserRefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRefreshTokens",
                schema: "Invoice");

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

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "FullName", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "Phone", "UserId" },
                values: new object[,]
                {
                    { new Guid("875b80bd-9c47-4a50-9410-6043fe3f01a8"), "تهران، خیابان اول، پلاک ۱", null, null, null, null, null, "شرکت الفبا", null, null, null, "02112345678", new Guid("6712adb7-a20d-43e9-8b29-357271f3bd65") },
                    { new Guid("8c1bf588-f1ce-400d-97d5-ec413300c9b0"), "تهران، خیابان اول، پلاک 2", null, null, null, null, null, "1شرکت الفبا", null, null, null, "02112345671", new Guid("92aa3814-ee96-4593-bdd3-cd613268137a") }
                });

            migrationBuilder.InsertData(
                schema: "Invoice",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedByIP", "CreatedByUserId", "DeletedByIP", "DeletedByUserId", "DeletedDateTime", "ModifiedByIP", "ModifiedByUserId", "ModifiedDateTime", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("45628e48-e0d3-4f2d-9786-d416d5abcf72"), "PRD-20260514-A1B2C3", null, null, null, null, null, null, null, null, "لپ تاپ مدل X1", 55000000, 15 },
                    { new Guid("62ad2886-3a1a-467d-ad65-8a64bb14a911"), "PRD-20260514-D4E5F6", null, null, null, null, null, null, null, null, "کیبورد مکانیکی RGB", 2500000, 50 }
                });
        }
    }
}
