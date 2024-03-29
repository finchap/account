﻿// <auto-generated />
using System;
using Finchap.Account.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Finchap.Account.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20180703201331_Remove schema")]
    partial class Removeschema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Finchap.Account.Domain.FinancialInstitutionAccount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Institution");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("FinancialInstitutionAccounts");
                });

            modelBuilder.Entity("Finchap.Account.Domain.TransactionalAccount", b =>
                {
                    b.Property<string>("Id")
                        .HasDefaultValue("1");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("AccountType")
                        .IsRequired();

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("FinancialInstitutionAccountId");

                    b.Property<DateTimeOffset>("LastRefresh");

                    b.Property<string>("TransactionalAccountId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FinancialInstitutionAccountId");

                    b.HasIndex("TransactionalAccountId");

                    b.ToTable("TransactionalAccounts");
                });

            modelBuilder.Entity("Finchap.Account.Domain.TransactionalAccount", b =>
                {
                    b.HasOne("Finchap.Account.Domain.FinancialInstitutionAccount")
                        .WithMany("Accounts")
                        .HasForeignKey("FinancialInstitutionAccountId");

                    b.HasOne("Finchap.Account.Domain.TransactionalAccount")
                        .WithMany()
                        .HasForeignKey("TransactionalAccountId");
                });
#pragma warning restore 612, 618
        }
    }
}
