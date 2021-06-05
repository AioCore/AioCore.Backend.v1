﻿// <auto-generated />
using System;
using AioCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AioCore.API.Migrations.AioDynamic
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

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DynamicAttributes");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicDateValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Value")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicDateValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Data")
                        .HasColumnType("xml");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("DynamicEntities");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicFloatValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicFloatValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicGuidValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Value")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicGuidValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicIntegerValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicIntegerValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicStringValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttributeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("EntityId");

                    b.ToTable("DynamicStringValues");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicDateValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicEntity", "Entity")
                        .WithMany("DynamicDateValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicFloatValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicEntity", "Entity")
                        .WithMany("DynamicFloatValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicGuidValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicEntity", "Entity")
                        .WithMany("DynamicGuidValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicIntegerValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicEntity", "Entity")
                        .WithMany("DynamicIntegerValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicStringValue", b =>
                {
                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AioCore.Domain.DynamicAggregatesModel.DynamicEntity", "Entity")
                        .WithMany("DynamicStringValues")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("AioCore.Domain.DynamicAggregatesModel.DynamicEntity", b =>
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
