using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grs.BioRestock.Infrastructure.Migrations
{
    public partial class _00004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DemandeSignature",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DemandeSignature");
        }
    }
}
