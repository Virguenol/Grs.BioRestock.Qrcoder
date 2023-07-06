using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grs.BioRestock.Infrastructure.Migrations
{
    public partial class _00002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DossierParentId",
                table: "DemandeSignature",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DemandeSignature_DossierParentId",
                table: "DemandeSignature",
                column: "DossierParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DemandeSignature_DemandeSignature_DossierParentId",
                table: "DemandeSignature",
                column: "DossierParentId",
                principalTable: "DemandeSignature",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DemandeSignature_DemandeSignature_DossierParentId",
                table: "DemandeSignature");

            migrationBuilder.DropIndex(
                name: "IX_DemandeSignature_DossierParentId",
                table: "DemandeSignature");

            migrationBuilder.DropColumn(
                name: "DossierParentId",
                table: "DemandeSignature");
        }
    }
}
