using Microsoft.EntityFrameworkCore.Migrations;

namespace Finchap.Account.Infrastructure.Migrations
{
  public partial class AddingFinanncialInstitution : Migration
  {
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_TransactionalAccounts_FinancialInstitutionAccounts_FinancialInstitutionAccountId",
          schema: "accounts",
          table: "TransactionalAccounts");

      migrationBuilder.DropTable(
          name: "FinancialInstitutionAccounts");

      migrationBuilder.DropIndex(
          name: "IX_TransactionalAccounts_FinancialInstitutionAccountId",
          schema: "accounts",
          table: "TransactionalAccounts");

      migrationBuilder.AlterColumn<string>(
          name: "FinancialInstitutionAccountId",
          schema: "accounts",
          table: "TransactionalAccounts",
          nullable: true,
          oldClrType: typeof(string),
          oldNullable: true);
    }

    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
          name: "FinancialInstitutionAccountId",
          schema: "accounts",
          table: "TransactionalAccounts",
          nullable: true,
          oldClrType: typeof(string),
          oldNullable: true);

      migrationBuilder.CreateTable(
          name: "FinancialInstitutionAccounts",
          columns: table => new
          {
            Id = table.Column<string>(nullable: false),
            Description = table.Column<string>(nullable: true),
            Institution = table.Column<string>(nullable: true),
            UserId = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_FinancialInstitutionAccounts", x => x.Id);
          });

      migrationBuilder.CreateIndex(
          name: "IX_TransactionalAccounts_FinancialInstitutionAccountId",
          schema: "accounts",
          table: "TransactionalAccounts",
          column: "FinancialInstitutionAccountId");

      migrationBuilder.AddForeignKey(
          name: "FK_TransactionalAccounts_FinancialInstitutionAccounts_FinancialInstitutionAccountId",
          schema: "accounts",
          table: "TransactionalAccounts",
          column: "FinancialInstitutionAccountId",
          principalTable: "FinancialInstitutionAccounts",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);
    }
  }
}