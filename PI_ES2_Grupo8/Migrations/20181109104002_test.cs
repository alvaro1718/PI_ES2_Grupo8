using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PI_ES2_Grupo8.Migrations
{
    public partial class test : Migration
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
                    Discricao = table.Column<string>(nullable: true),
                    TratamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnfermeirosId = table.Column<int>(nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TratamentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_Material_Tratamento_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamento",
                        principalColumn: "TratamentoId",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    UtenteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    Morada = table.Column<string>(nullable: true),
                    TipodeTratamento = table.Column<string>(nullable: true),
                    HorarioServicoDomicilioId = table.Column<int>(nullable: true),
                    TratamentoId = table.Column<int>(nullable: true)
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
                        name: "FK_Utente_Tratamento_TratamentoId",
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
                name: "IX_Material_TratamentoId",
                table: "Material",
                column: "TratamentoId");

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
                name: "IX_Utente_HorarioServicoDomicilioId",
                table: "Utente",
                column: "HorarioServicoDomicilioId");

            migrationBuilder.CreateIndex(
                name: "IX_Utente_TratamentoId",
                table: "Utente",
                column: "TratamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Utente");

            migrationBuilder.DropTable(
                name: "Tratamento");

            migrationBuilder.DropTable(
                name: "HorarioServicoDomicilio");

            migrationBuilder.DropTable(
                name: "Enfermeiros");
        }
    }
}
