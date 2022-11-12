using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementSystemBackend.Migrations
{
    public partial class sm_07082022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsUpdated",
                table: "UserMaster",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "UserMaster");
        }
    }
}
