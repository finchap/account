using Microsoft.EntityFrameworkCore.Migrations;

namespace Finchap.Account.Infrastructure.Migrations
{
  public partial class Removeschema : Migration
  {
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.EnsureSchema(
          name: "accounts");

      migrationBuilder.RenameTable(
          name: "TransactionalAccounts",
          newName: "TransactionalAccounts",
          newSchema: "accounts");
    }

    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameTable(
          name: "TransactionalAccounts",
          schema: "accounts",
          newName: "TransactionalAccounts");
    }
  }
}