using Microsoft.EntityFrameworkCore.Migrations;

namespace WealthManager.Server.Migrations
{
    public partial class RevertBacktoUtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "RecurringDeposits",
                newName: "LastUpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "MutualFundTxns",
                newName: "LastUpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "MutualFunds",
                newName: "LastUpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "MutualFundDetails",
                newName: "LastUpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "FixedDeposits",
                newName: "LastUpdatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOn",
                table: "Banks",
                newName: "LastUpdatedOnUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatedOnUtc",
                table: "RecurringDeposits",
                newName: "LastUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOnUtc",
                table: "MutualFundTxns",
                newName: "LastUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOnUtc",
                table: "MutualFunds",
                newName: "LastUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOnUtc",
                table: "MutualFundDetails",
                newName: "LastUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOnUtc",
                table: "FixedDeposits",
                newName: "LastUpdatedOn");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedOnUtc",
                table: "Banks",
                newName: "LastUpdatedOn");
        }
    }
}
