using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementSystemBackend.Migrations
{
    public partial class sm_30122022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantMaster",
                columns: table => new
                {
                    TenantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantMaster", x => x.TenantId);
                });

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
                    Priroty = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "FormulaMaster",
                columns: table => new
                {
                    FId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Formula = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulaMaster", x => x.FId);
                    table.ForeignKey(
                        name: "FK_FormulaMaster_TenantMaster_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantMaster",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleMaster",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMaster", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_RoleMaster_TenantMaster_TenantId",
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
                    MRP = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: true),
                    Default_Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountRate_Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Special_Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ActualPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FId = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_ProductMaster_FormulaMaster_FId",
                        column: x => x.FId,
                        principalTable: "FormulaMaster",
                        principalColumn: "FId");
                    table.ForeignKey(
                        name: "FK_ProductMaster_TenantMaster_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantMaster",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMaster",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ContactNumber = table.Column<long>(type: "bigint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMaster", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserMaster_RoleMaster_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleMaster",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMaster_TenantMaster_TenantId",
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
                name: "IX_FormulaMaster_Formula",
                table: "FormulaMaster",
                column: "Formula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormulaMaster_TenantId",
                table: "FormulaMaster",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_CompantyId",
                table: "ProductMaster",
                column: "CompantyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_FId",
                table: "ProductMaster",
                column: "FId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_ProductName",
                table: "ProductMaster",
                column: "ProductName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaster_TenantId",
                table: "ProductMaster",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMaster_RoleName_TenantId",
                table: "RoleMaster",
                columns: new[] { "RoleName", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleMaster_TenantId",
                table: "RoleMaster",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantMaster_TenantName",
                table: "TenantMaster",
                column: "TenantName",
                unique: true);

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
                name: "IX_UserMaster_RoleId",
                table: "UserMaster",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_TenantId",
                table: "UserMaster",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMaster_UserName",
                table: "UserMaster",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMaster");

            migrationBuilder.DropTable(
                name: "UserMaster");

            migrationBuilder.DropTable(
                name: "CompanyMaster");

            migrationBuilder.DropTable(
                name: "FormulaMaster");

            migrationBuilder.DropTable(
                name: "RoleMaster");

            migrationBuilder.DropTable(
                name: "TenantMaster");
        }
    }
}
