using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PI_ES2_Grupo8.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enfermeiros",
                columns: table => new
                {
                    EnfermeirosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    Especializacao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeiros", x => x.EnfermeirosId);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    MedicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.MedicoId);
                });

            migrationBuilder.CreateTable(
                name: "HorarioServicoDomicilio",
                columns: table => new
                {
                    HorarioServicoDomicilioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<int>(nullable: false),
                    Hora = table.Column<int>(nullable: false),
                    EnfermeirosId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioServicoDomicilio", x => x.HorarioServicoDomicilioId);
                    table.ForeignKey(
                        name: "FK_HorarioServicoDomicilio_Enfermeiros_EnfermeirosId",
                        column: x => x.EnfermeirosId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeirosId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tratamento",
                columns: table => new
                {
                    TratamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipodeTratamento = table.Column<string>(nullable: false),
                    HorarioServicoDomicilioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamento", x => x.TratamentoId);
                    table.ForeignKey(
                        name: "FK_Tratamento_HorarioServicoDomicilio_HorarioServicoDomicilioId",
                        column: x => x.HorarioServicoDomicilioId,
                        principalTable: "HorarioServicoDomicilio",
                        principalColumn: "HorarioServicoDomicilioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    UtenteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    HorarioServicoDomicilioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utente", x => x.UtenteId);
                    table.ForeignKey(
                        name: "FK_Utente_HorarioServicoDomicilio_HorarioServicoDomicilioId",
                        column: x => x.HorarioServicoDomicilioId,
                        principalTable: "HorarioServicoDomicilio",
                        principalColumn: "HorarioServicoDomicilioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receita",
                columns: table => new
                {
                    ReceitaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicoId = table.Column<int>(nullable: false),
                    UtenteId = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receita", x => x.ReceitaId);
                    table.ForeignKey(
                        name: "FK_Receita_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medico",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receita_Utente_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utente",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceitarTratamento",
                columns: table => new
                {
                    ReceitarTratamentoId = table.Column<int>(nullable: false),
                    ReceitaId = table.Column<int>(nullable: false),
                    TratamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitarTratamento", x => new { x.ReceitaId, x.TratamentoId });
                    table.ForeignKey(
                        name: "FK_ReceitarTratamento_Receita_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receita",
                        principalColumn: "ReceitaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitarTratamento_Tratamento_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamento",
                        principalColumn: "TratamentoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioServicoDomicilio_EnfermeirosId",
                table: "HorarioServicoDomicilio",
                column: "EnfermeirosId");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_MedicoId",
                table: "Receita",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Receita_UtenteId",
                table: "Receita",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitarTratamento_TratamentoId",
                table: "ReceitarTratamento",
                column: "TratamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_HorarioServicoDomicilioId",
                table: "Tratamento",
                column: "HorarioServicoDomicilioId");

            migrationBuilder.CreateIndex(
                name: "IX_Utente_HorarioServicoDomicilioId",
                table: "Utente",
                column: "HorarioServicoDomicilioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "ReceitarTratamento");

            migrationBuilder.DropTable(
                name: "Receita");

            migrationBuilder.DropTable(
                name: "Tratamento");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Utente");

            migrationBuilder.DropTable(
                name: "HorarioServicoDomicilio");

            migrationBuilder.DropTable(
                name: "Enfermeiros");
        }
    }
}
