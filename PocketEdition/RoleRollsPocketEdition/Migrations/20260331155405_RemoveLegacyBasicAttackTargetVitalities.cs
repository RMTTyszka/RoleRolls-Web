using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLegacyBasicAttackTargetVitalities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicAttackTargetFirstVitality_Id",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "BasicAttackTargetFirstVitality_Type",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "BasicAttackTargetSecondVitality_Id",
                table: "ItemConfigurations");

            migrationBuilder.DropColumn(
                name: "BasicAttackTargetSecondVitality_Type",
                table: "ItemConfigurations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BasicAttackTargetFirstVitality_Id",
                table: "ItemConfigurations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BasicAttackTargetFirstVitality_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BasicAttackTargetSecondVitality_Id",
                table: "ItemConfigurations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BasicAttackTargetSecondVitality_Type",
                table: "ItemConfigurations",
                type: "integer",
                nullable: true);
        }
    }
}
