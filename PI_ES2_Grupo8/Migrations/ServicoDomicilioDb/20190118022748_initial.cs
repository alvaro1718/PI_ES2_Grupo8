using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PI_ES2_Grupo8.Migrations.ServicoDomicilioDb
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialização",
                columns: table => new
                {
                    EspecializaçãoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialização", x => x.EspecializaçãoId);
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
                name: "Enfermeiros",
                columns: table => new
                {
                    EnfermeirosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    EspecializaçãoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeiros", x => x.EnfermeirosId);
                    table.ForeignKey(
                        name: "FK_Enfermeiros_Especialização_EspecializaçãoId",
                        column: x => x.EspecializaçãoId,
                        principalTable: "Especialização",
                        principalColumn: "EspecializaçãoId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "HorarioTrabalho",
                columns: table => new
                {
                    HorarioTrabalhoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    HoraInicio = table.Column<string>(nullable: true),
                    HoraFim = table.Column<string>(nullable: true),
                    EnfermeirosId = table.Column<int>(nullable: false),
                    Troca = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioTrabalho", x => x.HorarioTrabalhoId);
                    table.ForeignKey(
                        name: "FK_HorarioTrabalho_Enfermeiros_EnfermeirosId",
                        column: x => x.EnfermeirosId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeirosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tratamento",
                columns: table => new
                {
                    TratamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipodeTratamento = table.Column<string>(nullable: false),
                    EnfermeirosId = table.Column<int>(nullable: true),
                    HorarioServicoDomicilioId = table.Column<int>(nullable: true),
                    HorarioTrabalhoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamento", x => x.TratamentoId);
                    table.ForeignKey(
                        name: "FK_Tratamento_Enfermeiros_EnfermeirosId",
                        column: x => x.EnfermeirosId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeirosId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tratamento_HorarioServicoDomicilio_HorarioServicoDomicilioId",
                        column: x => x.HorarioServicoDomicilioId,
                        principalTable: "HorarioServicoDomicilio",
                        principalColumn: "HorarioServicoDomicilioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tratamento_HorarioTrabalho_HorarioTrabalhoId",
                        column: x => x.HorarioTrabalhoId,
                        principalTable: "HorarioTrabalho",
                        principalColumn: "HorarioTrabalhoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Troca",
                columns: table => new
                {
                    TrocaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Justificação = table.Column<string>(nullable: false),
                    EnfermeirosId = table.Column<int>(nullable: false),
                    EnfermeirosEId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    HorarioTrabalhoId = table.Column<int>(nullable: false),
                    HorarioTrabalhoAntigoId = table.Column<int>(nullable: false),
                    Aprovar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troca", x => x.TrocaId);
                    table.ForeignKey(
                        name: "FK_Troca_Enfermeiros_EnfermeirosEId",
                        column: x => x.EnfermeirosEId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeirosId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Troca_Enfermeiros_EnfermeirosId",
                        column: x => x.EnfermeirosId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeirosId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Troca_HorarioTrabalho_HorarioTrabalhoAntigoId",
                        column: x => x.HorarioTrabalhoAntigoId,
                        principalTable: "HorarioTrabalho",
                        principalColumn: "HorarioTrabalhoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Troca_HorarioTrabalho_HorarioTrabalhoId",
                        column: x => x.HorarioTrabalhoId,
                        principalTable: "HorarioTrabalho",
                        principalColumn: "HorarioTrabalhoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    UtenteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    N_Utente_Saude = table.Column<string>(nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Problemas = table.Column<string>(nullable: true),
                    HorarioServicoDomicilioId = table.Column<int>(nullable: true),
                    HorarioTrabalhoId = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Utente_HorarioTrabalho_HorarioTrabalhoId",
                        column: x => x.HorarioTrabalhoId,
                        principalTable: "HorarioTrabalho",
                        principalColumn: "HorarioTrabalhoId",
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
                    Date = table.Column<DateTime>(nullable: false),
                    Nreceita = table.Column<int>(nullable: false)
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
                    ReceitarTratamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceitaId = table.Column<int>(nullable: false),
                    TratamentoId = table.Column<int>(nullable: false),
                    DataTratamento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitarTratamento", x => x.ReceitarTratamentoId);
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
                name: "IX_Enfermeiros_EspecializaçãoId",
                table: "Enfermeiros",
                column: "EspecializaçãoId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioServicoDomicilio_EnfermeirosId",
                table: "HorarioServicoDomicilio",
                column: "EnfermeirosId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioTrabalho_EnfermeirosId",
                table: "HorarioTrabalho",
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
                name: "IX_ReceitarTratamento_ReceitaId",
                table: "ReceitarTratamento",
                column: "ReceitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitarTratamento_TratamentoId",
                table: "ReceitarTratamento",
                column: "TratamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_EnfermeirosId",
                table: "Tratamento",
                column: "EnfermeirosId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_HorarioServicoDomicilioId",
                table: "Tratamento",
                column: "HorarioServicoDomicilioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_HorarioTrabalhoId",
                table: "Tratamento",
                column: "HorarioTrabalhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_EnfermeirosEId",
                table: "Troca",
                column: "EnfermeirosEId");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_EnfermeirosId",
                table: "Troca",
                column: "EnfermeirosId");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_HorarioTrabalhoAntigoId",
                table: "Troca",
                column: "HorarioTrabalhoAntigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_HorarioTrabalhoId",
                table: "Troca",
                column: "HorarioTrabalhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Utente_HorarioServicoDomicilioId",
                table: "Utente",
                column: "HorarioServicoDomicilioId");

            migrationBuilder.CreateIndex(
                name: "IX_Utente_HorarioTrabalhoId",
                table: "Utente",
                column: "HorarioTrabalhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceitarTratamento");

            migrationBuilder.DropTable(
                name: "Troca");

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
                name: "HorarioTrabalho");

            migrationBuilder.DropTable(
                name: "Enfermeiros");

            migrationBuilder.DropTable(
                name: "Especialização");
        }
    }
}
