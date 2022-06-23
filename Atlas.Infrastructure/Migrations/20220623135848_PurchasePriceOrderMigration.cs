using Microsoft.EntityFrameworkCore.Migrations;

namespace Atlas.Persistence.Migrations
{
    public partial class PurchasePriceOrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "SellingPrice");

            migrationBuilder.AddColumn<float>(
                name: "PurchasePrice",
                table: "Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "Orders",
                newName: "Price");
        }
    }
}
