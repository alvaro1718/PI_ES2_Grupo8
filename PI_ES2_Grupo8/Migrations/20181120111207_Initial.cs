using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PI_ES2_Grupo8.Migrations
{
    public partial class Initial : Migration
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "Troca",
                columns: table => new
                {
                    TrocaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    justificação = table.Column<string>(nullable: false),
                    EnfermeirosId = table.Column<int>(nullable: true),
                    EnfermeiroId = table.Column<int>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    HorarioServicoDomicilioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troca", x => x.TrocaId);
                    table.ForeignKey(
                        name: "FK_Troca_Enfermeiros_EnfermeirosId",
                        column: x => x.EnfermeirosId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeirosId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Troca_HorarioServicoDomicilio_HorarioServicoDomicilioId",
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
                    Nome = table.Column<string>(nullable: true),
                    Morada = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
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
                name: "Tratamento",
                columns: table => new
                {
                    Discricao = table.Column<string>(nullable: true),
                    TratamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnfermeirosId = table.Column<int>(nullable: false),
                    UtenteId = table.Column<int>(nullable: false),
                    HorarioServicoDomicilioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamento", x => x.TratamentoId);
                    table.ForeignKey(
                        name: "FK_Tratamento_Enfermeiros_EnfermeirosId",
                        column: x => x.EnfermeirosId,
                        principalTable: "Enfermeiros",
                        principalColumn: "EnfermeirosId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamento_HorarioServicoDomicilio_HorarioServicoDomicilioId",
                        column: x => x.HorarioServicoDomicilioId,
                        principalTable: "HorarioServicoDomicilio",
                        principalColumn: "HorarioServicoDomicilioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tratamento_Utente_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utente",
                        principalColumn: "UtenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    ServicosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TratamentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.ServicosId);
                    table.ForeignKey(
                        name: "FK_Servicos_Tratamento_TratamentoId",
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
                name: "IX_Servicos_TratamentoId",
                table: "Servicos",
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
                name: "IX_Tratamento_UtenteId",
                table: "Tratamento",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_EnfermeirosId",
                table: "Troca",
                column: "EnfermeirosId");

            migrationBuilder.CreateIndex(
                name: "IX_Troca_HorarioServicoDomicilioId",
                table: "Troca",
                column: "HorarioServicoDomicilioId");

            migrationBuilder.CreateIndex(
                name: "IX_Utente_HorarioServicoDomicilioId",
                table: "Utente",
                column: "HorarioServicoDomicilioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Troca");

            migrationBuilder.DropTable(
                name: "Tratamento");

            migrationBuilder.DropTable(
                name: "Utente");

            migrationBuilder.DropTable(
                name: "HorarioServicoDomicilio");

            migrationBuilder.DropTable(
                name: "Enfermeiros");

            migrationBuilder.DropTable(
                name: "Especialização");
        }
    }
}
