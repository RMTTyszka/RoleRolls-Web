using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250514_082946 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueType",
                table: "Bonus",
                newName: "Origin");

            migrationBuilder.AlterColumn<string>(
                name: "CreatureTypeTitle",
                table: "CampaignTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArchetypeTitle",
                table: "CampaignTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Campaigns",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<int>(
                name: "Property_Type",
                table: "Bonus",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "Property_Id",
                table: "Bonus",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "Application",
                table: "Bonus",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureId",
                table: "Bonus",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_CreatureId",
                table: "Bonus",
                column: "CreatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_Creatures_CreatureId",
                table: "Bonus",
                column: "CreatureId",
                principalTable: "Creatures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_Creatures_CreatureId",
                table: "Bonus");

            migrationBuilder.DropIndex(
                name: "IX_Bonus_CreatureId",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "Application",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "CreatureId",
                table: "Bonus");

            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "Bonus",
                newName: "ValueType");

            migrationBuilder.AlterColumn<string>(
                name: "CreatureTypeTitle",
                table: "CampaignTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "ArchetypeTitle",
                table: "CampaignTemplates",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Campaigns",
                type: "character varying(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Property_Type",
                table: "Bonus",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Property_Id",
                table: "Bonus",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
