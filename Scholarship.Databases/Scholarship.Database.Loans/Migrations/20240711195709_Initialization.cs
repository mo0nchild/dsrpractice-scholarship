using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scholarship.Database.Loans.Migrations
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
                name: "LoanInfo",
                schema: "public",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    MoneyAmount = table.Column<double>(type: "double precision", nullable: false),
                    OpenTime = table.Column<DateOnly>(type: "date", nullable: false),
                    BeforeTime = table.Column<DateOnly>(type: "date", nullable: false),
                    CloseTime = table.Column<DateOnly>(type: "date", nullable: true),
                    CreditorSurname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreditorName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreditorPatronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanInfo", x => x.Uuid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanInfo_Uuid",
                schema: "public",
                table: "LoanInfo",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanInfo",
                schema: "public");
        }
    }
}
