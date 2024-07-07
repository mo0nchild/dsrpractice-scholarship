using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarship.Database.Users.Migrations
{
    /// <inheritdoc />
    public partial class Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "public",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                schema: "public",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    RoleUuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_UserInfo_UserRole_RoleUuid",
                        column: x => x.RoleUuid,
                        principalSchema: "public",
                        principalTable: "UserRole",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "public",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserInfoUuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_RefreshToken_UserInfo_UserInfoUuid",
                        column: x => x.UserInfoUuid,
                        principalSchema: "public",
                        principalTable: "UserInfo",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserInfoUuid",
                schema: "public",
                table: "RefreshToken",
                column: "UserInfoUuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Uuid",
                schema: "public",
                table: "RefreshToken",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_Email",
                schema: "public",
                table: "UserInfo",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_RoleUuid",
                schema: "public",
                table: "UserInfo",
                column: "RoleUuid");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_Uuid",
                schema: "public",
                table: "UserInfo",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Name",
                schema: "public",
                table: "UserRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Uuid",
                schema: "public",
                table: "UserRole",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserInfo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "public");
        }
    }
}
