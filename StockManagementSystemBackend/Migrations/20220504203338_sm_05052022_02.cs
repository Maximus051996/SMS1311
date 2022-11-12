using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementSystemBackend.Migrations
{
    public partial class sm_05052022_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserMaster",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserMaster",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "TenantMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "RoleMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_ContactNumber",
                table: "UserMaster",
                column: "ContactNumber",
                unique: true,
                filter: "[ContactNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_Email",
                table: "UserMaster",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_UserName",
                table: "UserMaster",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TenantMaster_TenantName",
                table: "TenantMaster",
                column: "TenantName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleMaster_RoleName",
                table: "RoleMaster",
                column: "RoleName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserMaster_ContactNumber",
                table: "UserMaster");

            migrationBuilder.DropIndex(
                name: "IX_UserMaster_Email",
                table: "UserMaster");

            migrationBuilder.DropIndex(
                name: "IX_UserMaster_UserName",
                table: "UserMaster");

            migrationBuilder.DropIndex(
                name: "IX_TenantMaster_TenantName",
                table: "TenantMaster");

            migrationBuilder.DropIndex(
                name: "IX_RoleMaster_RoleName",
                table: "RoleMaster");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserMaster");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserMaster",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserMaster",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "TenantMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "RoleMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
