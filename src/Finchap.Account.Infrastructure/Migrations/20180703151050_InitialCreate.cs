using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Finchap.Account.Infrastructure.Migrations
{
  public partial class InitialCreate : Migration
  {
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "TransactionalAccounts",
          schema: "accounts");
    }

    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.EnsureSchema(
          name: "accounts");

      migrationBuilder.CreateTable(
          name: "TransactionalAccounts",
          schema: "accounts",
          columns: table => new
          {
            Id = table.Column<string>(nullable: false, defaultValue: "1"),
            AccountNumber = table.Column<string>(maxLength: 200, nullable: false),
            AccountType = table.Column<string>(nullable: false),
            Description = table.Column<string>(maxLength: 200, nullable: true),
            FinancialInstitutionAccountId = table.Column<string>(nullable: true),
            LastRefresh = table.Column<DateTimeOffset>(nullable: false),
            UserId = table.Column<string>(nullable: false),
            TransactionalAccountId = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_TransactionalAccounts", x => x.Id);
            table.ForeignKey(
                      name: "FK_TransactionalAccounts_TransactionalAccounts_TransactionalAccountId",
                      column: x => x.TransactionalAccountId,
                      principalSchema: "accounts",
                      principalTable: "TransactionalAccounts",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateIndex(
          name: "IX_TransactionalAccounts_TransactionalAccountId",
          schema: "accounts",
          table: "TransactionalAccounts",
          column: "TransactionalAccountId");
    }
  }
}