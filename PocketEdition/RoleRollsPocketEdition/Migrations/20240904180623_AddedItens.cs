using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class AddedItens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PowerId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    Slot = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTemplates_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemTemplates_PowerTemplates_PowerId",
                        column: x => x.PowerId,
                        principalTable: "PowerTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PowerId = table.Column<Guid>(type: "uuid", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    Category = table.Column<int>(type: "integer", nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: true),
                    WeaponInstance_Category = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInstances_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemInstances_ItemTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "ItemTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemInstances_PowerTemplates_PowerId",
                        column: x => x.PowerId,
                        principalTable: "PowerTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    MainHandId = table.Column<Guid>(type: "uuid", nullable: true),
                    OffHandId = table.Column<Guid>(type: "uuid", nullable: true),
                    HeadId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChestId = table.Column<Guid>(type: "uuid", nullable: true),
                    FeetId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArmsId = table.Column<Guid>(type: "uuid", nullable: true),
                    WaistId = table.Column<Guid>(type: "uuid", nullable: true),
                    NeckId = table.Column<Guid>(type: "uuid", nullable: true),
                    LeftRingId = table.Column<Guid>(type: "uuid", nullable: true),
                    RightRingId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_ArmsId",
                        column: x => x.ArmsId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_ChestId",
                        column: x => x.ChestId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_FeetId",
                        column: x => x.FeetId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_HeadId",
                        column: x => x.HeadId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_LeftRingId",
                        column: x => x.LeftRingId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_MainHandId",
                        column: x => x.MainHandId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_NeckId",
                        column: x => x.NeckId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_OffHandId",
                        column: x => x.OffHandId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_RightRingId",
                        column: x => x.RightRingId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_WaistId",
                        column: x => x.WaistId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ArmsId",
                table: "Equipment",
                column: "ArmsId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ChestId",
                table: "Equipment",
                column: "ChestId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CreatureId",
                table: "Equipment",
                column: "CreatureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_FeetId",
                table: "Equipment",
                column: "FeetId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_HeadId",
                table: "Equipment",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LeftRingId",
                table: "Equipment",
                column: "LeftRingId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_MainHandId",
                table: "Equipment",
                column: "MainHandId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_NeckId",
                table: "Equipment",
                column: "NeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_OffHandId",
                table: "Equipment",
                column: "OffHandId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_RightRingId",
                table: "Equipment",
                column: "RightRingId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_WaistId",
                table: "Equipment",
                column: "WaistId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CreatureId",
                table: "Inventory",
                column: "CreatureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemInstances_InventoryId",
                table: "ItemInstances",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInstances_PowerId",
                table: "ItemInstances",
                column: "PowerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInstances_TemplateId",
                table: "ItemInstances",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTemplates_CampaignId",
                table: "ItemTemplates",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTemplates_PowerId",
                table: "ItemTemplates",
                column: "PowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "ItemInstances");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "ItemTemplates");
        }
    }
}
