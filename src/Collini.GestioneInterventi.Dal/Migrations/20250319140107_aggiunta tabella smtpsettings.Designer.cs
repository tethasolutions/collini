﻿// <auto-generated />
using System;
using Collini.GestioneInterventi.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Collini.GestioneInterventi.Dal.Migrations
{
    [DbContext(typeof(ColliniDbContext))]
    [Migration("20250319140107_aggiunta tabella smtpsettings")]
    partial class aggiuntatabellasmtpsettings
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Activity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<DateTimeOffset>("End")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("JobId")
                        .HasColumnType("bigint");

                    b.Property<long>("OperatorId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("Start")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("StatusChangedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("OperatorId");

                    b.ToTable("Activities", "Docs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Job", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<long?>("CustomerAddressId")
                        .HasColumnType("bigint");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("JobDate")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<long>("ProductTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("ResultNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SourceId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("StatusChangedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerAddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("SourceId");

                    b.ToTable("Jobs", "Docs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Note", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ActivityId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long?>("JobId")
                        .HasColumnType("bigint");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long?>("QuotationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("JobId");

                    b.HasIndex("OrderId");

                    b.HasIndex("QuotationId");

                    b.ToTable("Notes", "Docs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.NoteAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("NoteId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("NoteAttachments", "Docs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<DateTimeOffset?>("ExpirationDate")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("JobId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("StatusChangedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<long>("SupplierId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Orders", "Docs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Quotation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<DateTimeOffset?>("ExpirationDate")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("JobId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("StatusChangedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Quotations", "Docs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.QuotationAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("QuotationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("QuotationId")
                        .IsUnique();

                    b.ToTable("QuotationAttachments", "Docs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.Contact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Alert")
                        .HasColumnType("bit");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ErpCode")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("FiscalType")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Surname")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Telephone")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Contacts", "Registry");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.ContactAddress", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<long>("ContactId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("EditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("EditedOn")
                        .HasPrecision(3)
                        .HasColumnType("datetimeoffset(3)");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMainAddress")
                        .HasColumnType("bit");

                    b.Property<string>("Province")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("StreetAddress")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Telephone")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("ContactAddresses", "Registry");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.JobSource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("JobSources", "Registry");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.ProductType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes", "Registry");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.SmtpSettings", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SmtpSettings", "Registry");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Security.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ColorHex")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Surname")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Users", "Security");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccessToken = "a0f0a2ffd0f37c955fda023ed287c12fab375bfc0c3e58f96114c9eeb20066b0",
                            EmailAddress = "info@collini.it",
                            Enabled = true,
                            PasswordHash = "d96eb91d532452667aff24191055ebba05d8a30853367821502a55ca4b1532db",
                            Role = 0,
                            Salt = "f3064d73de0ca6b806ad24df65a59e1eb692393fc3f0b0297e37df522610b58b",
                            UserName = "administrator"
                        });
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Activity", b =>
                {
                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Job", "Job")
                        .WithMany("Activities")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Collini.GestioneInterventi.Domain.Security.User", "Operator")
                        .WithMany("Activities")
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Job", b =>
                {
                    b.HasOne("Collini.GestioneInterventi.Domain.Registry.ContactAddress", "CustomerAddress")
                        .WithMany("Jobs")
                        .HasForeignKey("CustomerAddressId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Collini.GestioneInterventi.Domain.Registry.Contact", "Customer")
                        .WithMany("Jobs")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Collini.GestioneInterventi.Domain.Registry.ProductType", "ProductType")
                        .WithMany("Jobs")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Collini.GestioneInterventi.Domain.Registry.JobSource", "Source")
                        .WithMany("Jobs")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("CustomerAddress");

                    b.Navigation("ProductType");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Note", b =>
                {
                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Activity", "Activity")
                        .WithMany("Notes")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Job", "Job")
                        .WithMany("Notes")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Order", "Order")
                        .WithMany("Notes")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Quotation", "Quotation")
                        .WithMany("Notes")
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Activity");

                    b.Navigation("Job");

                    b.Navigation("Order");

                    b.Navigation("Quotation");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.NoteAttachment", b =>
                {
                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Note", "Note")
                        .WithMany("Attachments")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Order", b =>
                {
                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Job", "Job")
                        .WithMany("Orders")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Collini.GestioneInterventi.Domain.Registry.Contact", "Supplier")
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Quotation", b =>
                {
                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Job", "Job")
                        .WithMany("Quotations")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.QuotationAttachment", b =>
                {
                    b.HasOne("Collini.GestioneInterventi.Domain.Docs.Quotation", "Quotation")
                        .WithOne("Attachment")
                        .HasForeignKey("Collini.GestioneInterventi.Domain.Docs.QuotationAttachment", "QuotationId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Quotation");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.ContactAddress", b =>
                {
                    b.HasOne("Collini.GestioneInterventi.Domain.Registry.Contact", "Contact")
                        .WithMany("Addresses")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Activity", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Job", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Notes");

                    b.Navigation("Orders");

                    b.Navigation("Quotations");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Note", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Order", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Docs.Quotation", b =>
                {
                    b.Navigation("Attachment");

                    b.Navigation("Notes");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.Contact", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Jobs");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.ContactAddress", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.JobSource", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Registry.ProductType", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("Collini.GestioneInterventi.Domain.Security.User", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
