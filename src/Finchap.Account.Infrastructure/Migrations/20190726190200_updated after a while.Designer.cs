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
    [Migration("20190726190200_updated after a while")]
    partial class updatedafterawhile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Finchap.Account.Domain.FIAccount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1");

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("FriendlyName");

                    b.Property<string>("Institution")
                        .IsRequired();

                    b.Property<string>("SecretNameId");

                    b.Property<int>("State");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FinancialInstitutionAccount");
                });

            modelBuilder.Entity("Finchap.Account.Domain.TRAccount", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("AccountType")
                        .IsRequired();

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("FIAccountId");

                    b.Property<string>("FinancialInstitutionAccountId");

                    b.Property<DateTimeOffset>("LastRefresh");

                    b.Property<string>("TRAccountId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FIAccountId");

                    b.HasIndex("TRAccountId");

                    b.ToTable("TransactionalAccounts");
                });

            modelBuilder.Entity("Finchap.Account.Domain.TRAccount", b =>
                {
                    b.HasOne("Finchap.Account.Domain.FIAccount")
                        .WithMany("Accounts")
                        .HasForeignKey("FIAccountId");

                    b.HasOne("Finchap.Account.Domain.TRAccount")
                        .WithMany()
                        .HasForeignKey("TRAccountId");
                });
#pragma warning restore 612, 618
        }
    }
}
