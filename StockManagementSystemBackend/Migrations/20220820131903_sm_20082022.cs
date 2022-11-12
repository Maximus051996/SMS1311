using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementSystemBackend.Migrations
{
    public partial class sm_20082022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyMaster",
                columns: table => new
                {
                    CompanyId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMaster", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyMaster_TenantMaster_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantMaster",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaster",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompantyId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: true),
                    MRP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Formula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaster", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductMaster_CompanyMaster_CompantyId",
                        column: x => x.CompantyId,
                        principalTable: "CompanyMaster",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductMaster_TenantMaster_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantMaster",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMaster_CompanyName",
                table: "CompanyMaster",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMaster_TenantId",
                table: "CompanyMaster",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_CompantyId",
                table: "ProductMaster",
                column: "CompantyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_ProductName",
                table: "ProductMaster",
                column: "ProductName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_TenantId",
                table: "ProductMaster",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMaster");

            migrationBuilder.DropTable(
                name: "CompanyMaster");
        }
    }
}
