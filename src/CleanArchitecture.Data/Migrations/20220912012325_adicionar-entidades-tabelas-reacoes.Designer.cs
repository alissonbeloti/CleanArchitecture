﻿// <auto-generated />
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanArchitecture.Data.Migrations
{
    [DbContext(typeof(StreamerDbContext))]
    [Migration("20220912012325_adicionar-entidades-tabelas-reacoes")]
    partial class adicionarentidadestabelasreacoes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ActorVideo", b =>
                {
                    b.Property<int>("AtoresId")
                        .HasColumnType("int");

                    b.Property<int>("VideosId")
                        .HasColumnType("int");

                    b.HasKey("AtoresId", "VideosId");

                    b.HasIndex("VideosId");

                    b.ToTable("VideoAtor", (string)null);
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sobrenome")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VideoId")
                        .IsUnique();

                    b.ToTable("Director");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Streamer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Streamers");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StreamerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StreamerId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("ActorVideo", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Actor", null)
                        .WithMany()
                        .HasForeignKey("AtoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Video", null)
                        .WithMany()
                        .HasForeignKey("VideosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Director", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Video", "Video")
                        .WithOne("Director")
                        .HasForeignKey("CleanArchitecture.Domain.Director", "VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Video");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Video", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Streamer", "Streamer")
                        .WithMany("Videos")
                        .HasForeignKey("StreamerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Streamer");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Streamer", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Video", b =>
                {
                    b.Navigation("Director")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
