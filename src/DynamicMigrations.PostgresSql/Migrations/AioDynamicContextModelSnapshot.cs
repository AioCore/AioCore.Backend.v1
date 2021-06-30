﻿// <auto-generated />
using System;
using AioCore.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DynamicMigrations.PostgresSql.Migrations
{
    [DbContext(typeof(AioDynamicContext))]
    partial class AioDynamicContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("DynamicAttributes");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicDateValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("Value")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicDateValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Data")
                        .HasColumnType("xml");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("DynamicEntities");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicFloatValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicFloatValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicGuidValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Value")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicGuidValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicIntegerValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicIntegerValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicStringValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicStringValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicDateValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicEntity", "Entity")
                        .WithMany("DynamicDateValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicFloatValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicEntity", "Entity")
                        .WithMany("DynamicFloatValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicGuidValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicEntity", "Entity")
                        .WithMany("DynamicGuidValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicIntegerValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicEntity", "Entity")
                        .WithMany("DynamicIntegerValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicStringValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicEntities.DynamicEntity", "Entity")
                        .WithMany("DynamicStringValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicEntities.DynamicEntity", b =>
                {
                    b.Navigation("DynamicDateValues");

                    b.Navigation("DynamicFloatValues");

                    b.Navigation("DynamicGuidValues");

                    b.Navigation("DynamicIntegerValues");

                    b.Navigation("DynamicStringValues");
                });
#pragma warning restore 612, 618
        }
    }
}
