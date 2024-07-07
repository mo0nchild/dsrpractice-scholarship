using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarship.Database.History.Migrations
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
                name: "ClosedLoanInfo",
                schema: "public",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    MoneyAmount = table.Column<double>(type: "double precision", nullable: false),
                    OpenTime = table.Column<DateOnly>(type: "date", nullable: false),
                    BeforeTime = table.Column<DateOnly>(type: "date", nullable: false),
                    ClosedTime = table.Column<DateOnly>(type: "date", nullable: false),
                    CreditorSurname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreditorName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreditorPatronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedLoanInfo", x => x.Uuid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClosedLoanInfo_Uuid",
                schema: "public",
                table: "ClosedLoanInfo",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosedLoanInfo",
                schema: "public");
        }
    }
}
