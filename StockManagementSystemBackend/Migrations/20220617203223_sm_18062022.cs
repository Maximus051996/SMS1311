using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementSystemBackend.Migrations
{
    public partial class sm_18062022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoleMaster_RoleName",
                table: "RoleMaster");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMaster_RoleName_TenantId",
                table: "RoleMaster",
                columns: new[] { "RoleName", "TenantId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoleMaster_RoleName_TenantId",
                table: "RoleMaster");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMaster_RoleName",
                table: "RoleMaster",
                column: "RoleName",
                unique: true);
        }
    }
}
