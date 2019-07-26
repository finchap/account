using Microsoft.EntityFrameworkCore.Migrations;

namespace Finchap.Account.Infrastructure.Migrations
{
    public partial class updatedafterawhile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "FinancialInstitutionAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecretNameId",
                table: "FinancialInstitutionAccount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "FinancialInstitutionAccount");

            migrationBuilder.DropColumn(
                name: "SecretNameId",
                table: "FinancialInstitutionAccount");
        }
    }
}
