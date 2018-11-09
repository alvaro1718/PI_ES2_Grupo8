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
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Enfermeiros", b =>
                {
                    b.Property<int>("EnfermeirosId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Especializacao")
                        .IsRequired();

                    b.Property<string>("Morada")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Telefone")
                        .IsRequired();

                    b.HasKey("EnfermeirosId");

                    b.ToTable("Enfermeiros");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.HorarioServicoDomicilio", b =>
                {
                    b.Property<int>("HorarioServicoDomicilioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Data");

                    b.Property<int?>("EnfermeirosId");

                    b.Property<int>("Hora");

                    b.HasKey("HorarioServicoDomicilioId");

                    b.HasIndex("EnfermeirosId");

                    b.ToTable("HorarioServicoDomicilio");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TratamentoId");

                    b.HasKey("MaterialId");

                    b.HasIndex("TratamentoId");

                    b.ToTable("Material");
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

                    b.Property<int?>("HorarioServicoDomicilioId");

                    b.HasKey("TratamentoId");

                    b.HasIndex("EnfermeirosId");

                    b.HasIndex("HorarioServicoDomicilioId");

                    b.ToTable("Tratamento");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Utente", b =>
                {
                    b.Property<int>("UtenteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("HorarioServicoDomicilioId");

                    b.Property<string>("Morada");

                    b.Property<string>("Name");

                    b.Property<string>("Sexo");

                    b.Property<string>("Telefone");

                    b.Property<int?>("TratamentoId");

                    b.HasKey("UtenteId");

                    b.HasIndex("HorarioServicoDomicilioId");

                    b.HasIndex("TratamentoId");

                    b.ToTable("Utente");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.HorarioServicoDomicilio", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.Enfermeiros", "Enfermeiros")
                        .WithMany()
                        .HasForeignKey("EnfermeirosId");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Material", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.Tratamento")
                        .WithMany("Materials")
                        .HasForeignKey("TratamentoId");
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
                        .WithMany()
                        .HasForeignKey("EnfermeirosId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PI_ES2_Grupo8.Models.HorarioServicoDomicilio")
                        .WithMany("Tratamentos")
                        .HasForeignKey("HorarioServicoDomicilioId");
                });

            modelBuilder.Entity("PI_ES2_Grupo8.Models.Utente", b =>
                {
                    b.HasOne("PI_ES2_Grupo8.Models.HorarioServicoDomicilio")
                        .WithMany("Utente")
                        .HasForeignKey("HorarioServicoDomicilioId");

                    b.HasOne("PI_ES2_Grupo8.Models.Tratamento")
                        .WithMany("Utentes")
                        .HasForeignKey("TratamentoId");
                });
#pragma warning restore 612, 618
        }
    }
}
