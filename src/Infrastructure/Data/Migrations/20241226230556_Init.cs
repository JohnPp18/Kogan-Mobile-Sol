using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kogan.Mobile.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessPartners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ObjectKey = table.Column<string>(type: "TEXT", maxLength: 16, nullable: true),
                    ObjectType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    DefComPercent = table.Column<decimal>(type: "TEXT", nullable: true, defaultValue: 86m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessPartners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanDurationDays = table.Column<int>(type: "INTEGER", nullable: false),
                    SimType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    WebSku = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupplierBatchId = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TotalQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierComPrcnt = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdSupplier = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 254, nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RedemptionDateEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlanSize = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batches_BusinessPartners_IdSupplier",
                        column: x => x.IdSupplier,
                        principalTable: "BusinessPartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchVoucherAssociation",
                columns: table => new
                {
                    IdBatch = table.Column<int>(type: "INTEGER", nullable: false),
                    IdVoucher = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchVoucherAssociation", x => new { x.IdBatch, x.IdVoucher });
                    table.ForeignKey(
                        name: "FK_BatchVoucherAssociation_Batches_IdBatch",
                        column: x => x.IdBatch,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchVoucherAssociation_Vouchers_IdVoucher",
                        column: x => x.IdVoucher,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoucherPins",
                columns: table => new
                {
                    IdBatch = table.Column<int>(type: "INTEGER", nullable: false),
                    IdVoucher = table.Column<int>(type: "INTEGER", nullable: false),
                    PinNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Msisdn = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    IsSold = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsRedeemed = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsExpired = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherPins", x => new { x.IdBatch, x.IdVoucher, x.PinNumber });
                    table.ForeignKey(
                        name: "FK_VoucherPins_BatchVoucherAssociation_IdBatch_IdVoucher",
                        columns: x => new { x.IdBatch, x.IdVoucher },
                        principalTable: "BatchVoucherAssociation",
                        principalColumns: new[] { "IdBatch", "IdVoucher" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_IdSupplier",
                table: "Batches",
                column: "IdSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_SupplierBatchId",
                table: "Batches",
                column: "SupplierBatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchVoucherAssociation_IdVoucher",
                table: "BatchVoucherAssociation",
                column: "IdVoucher");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessPartners_ObjectType_ObjectKey",
                table: "BusinessPartners",
                columns: new[] { "ObjectType", "ObjectKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_WebSku",
                table: "Vouchers",
                column: "WebSku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherPins");

            migrationBuilder.DropTable(
                name: "BatchVoucherAssociation");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "BusinessPartners");
        }
    }
}
