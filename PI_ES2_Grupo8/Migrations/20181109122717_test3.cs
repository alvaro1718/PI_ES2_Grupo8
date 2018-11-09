using Microsoft.EntityFrameworkCore.Migrations;

namespace PI_ES2_Grupo8.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utente_Tratamento_TratamentoId",
                table: "Utente");

            migrationBuilder.DropIndex(
                name: "IX_Utente_TratamentoId",
                table: "Utente");

            migrationBuilder.DropColumn(
                name: "TratamentoId",
                table: "Utente");

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "Utente",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Utente",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "UtenteId",
                table: "Tratamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_UtenteId",
                table: "Tratamento",
                column: "UtenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tratamento_Utente_UtenteId",
                table: "Tratamento",
                column: "UtenteId",
                principalTable: "Utente",
                principalColumn: "UtenteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tratamento_Utente_UtenteId",
                table: "Tratamento");

            migrationBuilder.DropIndex(
                name: "IX_Tratamento_UtenteId",
                table: "Tratamento");

            migrationBuilder.DropColumn(
                name: "UtenteId",
                table: "Tratamento");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Utente",
                newName: "Sexo");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Utente",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "TratamentoId",
                table: "Utente",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utente_TratamentoId",
                table: "Utente",
                column: "TratamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Utente_Tratamento_TratamentoId",
                table: "Utente",
                column: "TratamentoId",
                principalTable: "Tratamento",
                principalColumn: "TratamentoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
