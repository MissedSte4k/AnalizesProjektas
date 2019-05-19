using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnalizesProjektas.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GateTime",
                columns: table => new
                {
                    GateTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Diena = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateTime", x => x.GateTimeId);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImonesPavadinimas = table.Column<string>(nullable: true),
                    TelefonoNr = table.Column<string>(nullable: true),
                    VardasPavarde = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    SupplierLink = table.Column<string>(nullable: true),
                    Busena = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: true),
                    GateTimeId = table.Column<int>(nullable: true),
                    DriverId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                    table.ForeignKey(
                        name: "FK_Shipments_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_GateTime_GateTimeId",
                        column: x => x.GateTimeId,
                        principalTable: "GateTime",
                        principalColumn: "GateTimeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Delay",
                columns: table => new
                {
                    DelayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VelavimoLaikas = table.Column<DateTime>(nullable: false),
                    NaujasAtvykimoLaikas = table.Column<DateTime>(nullable: false),
                    ShipmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delay", x => x.DelayId);
                    table.ForeignKey(
                        name: "FK_Delay_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SendingProducts",
                columns: table => new
                {
                    SendingProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ShipmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendingProducts", x => x.SendingProductId);
                    table.ForeignKey(
                        name: "FK_SendingProducts_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delay_ShipmentId",
                table: "Delay",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SendingProducts_ShipmentId",
                table: "SendingProducts",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_DriverId",
                table: "Shipments",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_GateTimeId",
                table: "Shipments",
                column: "GateTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_SupplierId",
                table: "Shipments",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delay");

            migrationBuilder.DropTable(
                name: "SendingProducts");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "GateTime");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
