using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculator.Data.Migrations
{
    public partial class AddedTaxBandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxBands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LowerLimit = table.Column<int>(type: "integer", nullable: false),
                    UpperLimit = table.Column<int>(type: "integer", nullable: false),
                    TaxRate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBands", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxBands");
        }
    }
}
