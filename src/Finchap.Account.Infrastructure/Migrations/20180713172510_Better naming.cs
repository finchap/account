using Microsoft.EntityFrameworkCore.Migrations;

namespace Finchap.Account.Infrastructure.Migrations
{
    public partial class Betternaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalAccounts_FinancialInstitutionAccounts_FinancialInstitutionAccountId",
                table: "TransactionalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalAccounts_TransactionalAccounts_TransactionalAccountId",
                table: "TransactionalAccounts");

            migrationBuilder.DropTable(
                name: "FinancialInstitutionAccounts");

            migrationBuilder.DropIndex(
                name: "IX_TransactionalAccounts_FinancialInstitutionAccountId",
                table: "TransactionalAccounts");

            migrationBuilder.RenameColumn(
                name: "TransactionalAccountId",
                table: "TransactionalAccounts",
                newName: "TRAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalAccounts_TransactionalAccountId",
                table: "TransactionalAccounts",
                newName: "IX_TransactionalAccounts_TRAccountId");

            migrationBuilder.AlterColumn<string>(
                name: "FinancialInstitutionAccountId",
                table: "TransactionalAccounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TransactionalAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "1");

            migrationBuilder.AddColumn<string>(
                name: "FIAccountId",
                table: "TransactionalAccounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FinancialInstitutionAccount",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValue: "1"),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    FriendlyName = table.Column<string>(nullable: true),
                    Institution = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialInstitutionAccount", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionalAccounts_FIAccountId",
                table: "TransactionalAccounts",
                column: "FIAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalAccounts_FinancialInstitutionAccount_FIAccountId",
                table: "TransactionalAccounts",
                column: "FIAccountId",
                principalTable: "FinancialInstitutionAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalAccounts_TransactionalAccounts_TRAccountId",
                table: "TransactionalAccounts",
                column: "TRAccountId",
                principalTable: "TransactionalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalAccounts_FinancialInstitutionAccount_FIAccountId",
                table: "TransactionalAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionalAccounts_TransactionalAccounts_TRAccountId",
                table: "TransactionalAccounts");

            migrationBuilder.DropTable(
                name: "FinancialInstitutionAccount");

            migrationBuilder.DropIndex(
                name: "IX_TransactionalAccounts_FIAccountId",
                table: "TransactionalAccounts");

            migrationBuilder.DropColumn(
                name: "FIAccountId",
                table: "TransactionalAccounts");

            migrationBuilder.RenameColumn(
                name: "TRAccountId",
                table: "TransactionalAccounts",
                newName: "TransactionalAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionalAccounts_TRAccountId",
                table: "TransactionalAccounts",
                newName: "IX_TransactionalAccounts_TransactionalAccountId");

            migrationBuilder.AlterColumn<string>(
                name: "FinancialInstitutionAccountId",
                table: "TransactionalAccounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TransactionalAccounts",
                nullable: false,
                defaultValue: "1",
                oldClrType: typeof(string));

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
                table: "TransactionalAccounts",
                column: "FinancialInstitutionAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalAccounts_FinancialInstitutionAccounts_FinancialInstitutionAccountId",
                table: "TransactionalAccounts",
                column: "FinancialInstitutionAccountId",
                principalTable: "FinancialInstitutionAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionalAccounts_TransactionalAccounts_TransactionalAccountId",
                table: "TransactionalAccounts",
                column: "TransactionalAccountId",
                principalTable: "TransactionalAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
