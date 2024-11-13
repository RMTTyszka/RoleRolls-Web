using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class addedHandsToEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HandsId",
                table: "Equipment",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_HandsId",
                table: "Equipment",
                column: "HandsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_ItemInstances_HandsId",
                table: "Equipment",
                column: "HandsId",
                principalTable: "ItemInstances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_ItemInstances_HandsId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_HandsId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "HandsId",
                table: "Equipment");
        }
    }
}
