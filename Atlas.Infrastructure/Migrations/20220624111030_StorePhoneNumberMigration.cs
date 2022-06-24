using Microsoft.EntityFrameworkCore.Migrations;

namespace Atlas.Persistence.Migrations
{
    public partial class StorePhoneNumberMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Stores",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Stores");
        }
    }
}
