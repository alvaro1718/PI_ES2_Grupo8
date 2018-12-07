﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PI_ES2_Grupo8.Models;

namespace PI_ES2_Grupo8.Migrations
{
    [DbContext(typeof(ServicoDomicilioDbContext))]
    partial class ServicoDomicilioDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PI_ES2_Grupo8.Models.EnfermeiroEscolhido", b =>
                {
                    b.Property<int>("EnfermeiroEscolhidoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnfermeirosId");

                    b.HasKey("EnfermeiroEscolhidoId");

                    b.HasIndex("EnfermeirosId");

                    b.ToTable("EnfermeiroEscolhido");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.EnfermeiroRequerente", b =>
                {
                    b.Property<int>("EnfermeiroRequerenteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnfermeirosId");

                    b.HasKey("EnfermeiroRequerenteId");

                    b.HasIndex("EnfermeirosId");

                    b.ToTable("EnfermeiroRequerente");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Enfermeiros", b =>
                {
                    b.Property<int>("EnfermeirosId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("EspecializaçãoId");

                    b.Property<string>("Morada")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Telefone")
                        .IsRequired();

                    b.HasKey("EnfermeirosId");

                    b.HasIndex("EspecializaçãoId");

                    b.ToTable("Enfermeiros");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Especialização", b =>
                {
                    b.Property<int>("EspecializaçãoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("EspecializaçãoId");

                    b.ToTable("Especialização");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.HorarioTrabalho", b =>
                {
                    b.Property<int>("HorarioTrabalhoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data");

                    b.Property<int>("EnfermeirosId");

                    b.Property<string>("HoraFim");

                    b.Property<string>("HoraInicio");

                    b.Property<bool>("Troca");

                    b.HasKey("HorarioTrabalhoId");

                    b.HasIndex("EnfermeirosId");

                    b.ToTable("HorarioTrabalho");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Servicos", b =>
                {
                    b.Property<int>("ServicosId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TratamentoId");

                    b.HasKey("ServicosId");

                    b.HasIndex("TratamentoId");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Tratamento", b =>
                {
                    b.Property<int>("TratamentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discricao");

                    b.Property<int>("EnfermeirosId");

                    b.Property<int?>("HorarioTrabalhoId");

                    b.Property<int>("UtenteId");

                    b.HasKey("TratamentoId");

                    b.HasIndex("EnfermeirosId");

                    b.HasIndex("HorarioTrabalhoId");

                    b.HasIndex("UtenteId");

                    b.ToTable("Tratamento");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Troca", b =>
                {
                    b.Property<int>("TrocaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data");

                    b.Property<int?>("EnfermeiroEscolhidoId");

                    b.Property<int?>("EnfermeiroRequerenteId");

                    b.Property<int>("EnfermeirosId");

                    b.Property<int>("HorarioTrabalhoId");

                    b.Property<string>("Justificação")
                        .IsRequired();

                    b.HasKey("TrocaId");

                    b.HasIndex("EnfermeiroEscolhidoId");

                    b.HasIndex("EnfermeiroRequerenteId");

                    b.HasIndex("EnfermeirosId");

                    b.HasIndex("HorarioTrabalhoId");

                    b.ToTable("Troca");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Utente", b =>
                {
                    b.Property<int>("UtenteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<int?>("HorarioTrabalhoId");

                    b.Property<string>("Morada");

                    b.Property<string>("Nome");

                    b.Property<string>("Telefone");

                    b.HasKey("UtenteId");

                    b.HasIndex("HorarioTrabalhoId");

                    b.ToTable("Utente");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.EnfermeiroEscolhido", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.Enfermeiros", "Enfermeiros")
                        .WithMany()
                        .HasForeignKey("EnfermeirosId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.EnfermeiroRequerente", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.Enfermeiros", "Enfermeiros")
                        .WithMany()
                        .HasForeignKey("EnfermeirosId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Enfermeiros", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.Especialização", "Especialização")
                        .WithMany("Enfermeiros")
                        .HasForeignKey("EspecializaçãoId");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.HorarioTrabalho", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.Enfermeiros", "Enfermeiros")
                        .WithMany()
                        .HasForeignKey("EnfermeirosId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Servicos", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.Tratamento")
                        .WithMany("Servicos")
                        .HasForeignKey("TratamentoId");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Tratamento", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.Enfermeiros", "Enfermeiros")
                        .WithMany("Tratamentos")
                        .HasForeignKey("EnfermeirosId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PI_ES2_Grupo8.Models.HorarioTrabalho")
                        .WithMany("Tratamentos")
                        .HasForeignKey("HorarioTrabalhoId");

                    b.HasOne("PI_ES2_Grupo8.Models.Utente", "utente")
                        .WithMany()
                        .HasForeignKey("UtenteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Troca", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.EnfermeiroEscolhido")
                        .WithMany("Trocas")
                        .HasForeignKey("EnfermeiroEscolhidoId");

                    b.HasOne("PI_ES2_Grupo8.Models.EnfermeiroRequerente")
                        .WithMany("Trocas")
                        .HasForeignKey("EnfermeiroRequerenteId");

                    b.HasOne("PI_ES2_Grupo8.Models.Enfermeiros", "EnfermeiroRequerente")
                        .WithMany("Trocas")
                        .HasForeignKey("EnfermeirosId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PI_ES2_Grupo8.Models.HorarioTrabalho", "HorarioTrabalhoNovo")
                        .WithMany("Trocas")
                        .HasForeignKey("HorarioTrabalhoId");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Utente", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.HorarioTrabalho")
                        .WithMany("Utente")
                        .HasForeignKey("HorarioTrabalhoId");
                });
#pragma warning restore 612, 618
        }
    }
}
