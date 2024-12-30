using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RoleRollsPocketEdition.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TotalAttributePoints = table.Column<int>(type: "integer", nullable: false),
                    TotalSkillsPoints = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Creatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxState",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uuid", nullable: false),
                    LockId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    Received = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReceiveCount = table.Column<int>(type: "integer", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Consumed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Delivered = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxState", x => x.Id);
                    table.UniqueConstraint("AK_InboxState_MessageId_ConsumerId", x => new { x.MessageId, x.ConsumerId });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                columns: table => new
                {
                    SequenceNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnqueueTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SentTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Headers = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    InboxMessageId = table.Column<Guid>(type: "uuid", nullable: true),
                    InboxConsumerId = table.Column<Guid>(type: "uuid", nullable: true),
                    OutboxId = table.Column<Guid>(type: "uuid", nullable: true),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: true),
                    InitiatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DestinationAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ResponseAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FaultAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.SequenceNumber);
                });

            migrationBuilder.CreateTable(
                name: "OutboxState",
                columns: table => new
                {
                    OutboxId = table.Column<Guid>(type: "uuid", nullable: false),
                    LockId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Delivered = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxState", x => x.OutboxId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeTemplates_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationSecret = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefenseTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Formula = table.Column<string>(type: "text", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefenseTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefenseTemplates_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Formula = table.Column<string>(type: "text", nullable: false),
                    CreatureTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeTemplates_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    AttributeTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_AttributeTemplates_AttributeTemplateId",
                        column: x => x.AttributeTemplateId,
                        principalTable: "AttributeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attributes_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AttributeTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillTemplates_AttributeTemplates_AttributeTemplateId",
                        column: x => x.AttributeTemplateId,
                        principalTable: "AttributeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillTemplates_CampaignTemplates_CampaignTemplateId",
                        column: x => x.CampaignTemplateId,
                        principalTable: "CampaignTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CampaignPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: true),
                    InvidationCode = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignPlayers_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignScenes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignScenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignScenes_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArmorDefenseId = table.Column<Guid>(type: "uuid", nullable: true),
                    BasicAttackTargetLifeId = table.Column<Guid>(type: "uuid", nullable: true),
                    LightWeaponHitPropertyId = table.Column<Guid>(type: "uuid", nullable: true),
                    MediumWeaponHitPropertyId = table.Column<Guid>(type: "uuid", nullable: true),
                    HeavyWeaponHitPropertyId = table.Column<Guid>(type: "uuid", nullable: true),
                    LightWeaponDamagePropertyId = table.Column<Guid>(type: "uuid", nullable: true),
                    MediumWeaponDamagePropertyId = table.Column<Guid>(type: "uuid", nullable: true),
                    HeavyWeaponDamagePropertyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemConfigurations_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PowerDurationType = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    ActionType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CastFormula = table.Column<string>(type: "text", nullable: false),
                    CastDescription = table.Column<string>(type: "text", nullable: false),
                    UseAttributeId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetDefenseId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsagesFormula = table.Column<string>(type: "text", nullable: false),
                    UsageType = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerTemplates_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Defenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Formula = table.Column<string>(type: "text", nullable: false),
                    DefenseTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Defenses_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Defenses_DefenseTemplates_DefenseTemplateId",
                        column: x => x.DefenseTemplateId,
                        principalTable: "DefenseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lifes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaxValue = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Formula = table.Column<string>(type: "text", nullable: false),
                    LifeTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lifes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lifes_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lifes_LifeTemplates_LifeTemplateId",
                        column: x => x.LifeTemplateId,
                        principalTable: "LifeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MinorSkillTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SkillTemplateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorSkillTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinorSkillTemplates_SkillTemplates_SkillTemplateId",
                        column: x => x.SkillTemplateId,
                        principalTable: "SkillTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skills_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Skills_SkillTemplates_SkillTemplateId",
                        column: x => x.SkillTemplateId,
                        principalTable: "SkillTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SceneId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorType = table.Column<int>(type: "integer", nullable: false),
                    RolledDices = table.Column<string>(type: "text", nullable: false),
                    NumberOfDices = table.Column<int>(type: "integer", nullable: false),
                    RollBonus = table.Column<int>(type: "integer", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyType = table.Column<int>(type: "integer", nullable: false),
                    NumberOfSuccesses = table.Column<int>(type: "integer", nullable: false),
                    NumberOfCriticalSuccesses = table.Column<int>(type: "integer", nullable: false),
                    NumberOfCriticalFailures = table.Column<int>(type: "integer", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    Complexity = table.Column<int>(type: "integer", nullable: false),
                    Success = table.Column<bool>(type: "boolean", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rolls_CampaignScenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "CampaignScenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SceneActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SceneId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorType = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SceneActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SceneActions_CampaignScenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "CampaignScenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SceneCreatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SceneId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatureType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SceneCreatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SceneCreatures_CampaignScenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "CampaignScenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreaturePowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    PowerTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsumedUsages = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreaturePowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreaturePowers_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreaturePowers_PowerTemplates_PowerTemplateId",
                        column: x => x.PowerTemplateId,
                        principalTable: "PowerTemplates",
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
                    Category = table.Column<int>(type: "integer", nullable: true),
                    Slot = table.Column<int>(type: "integer", nullable: true),
                    WeaponTemplate_Category = table.Column<int>(type: "integer", nullable: true),
                    DamageType = table.Column<int>(type: "integer", nullable: true)
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
                name: "PowerInstance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UsedCharges = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerInstance_PowerTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "PowerTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MinorSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MinorSkillTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    CreatureId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinorSkills_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MinorSkills_MinorSkillTemplates_MinorSkillTemplateId",
                        column: x => x.MinorSkillTemplateId,
                        principalTable: "MinorSkillTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinorSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PowerId = table.Column<Guid>(type: "uuid", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    ItemType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInstances_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    HandsId = table.Column<Guid>(type: "uuid", nullable: true),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_ChestId",
                        column: x => x.ChestId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_FeetId",
                        column: x => x.FeetId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_HandsId",
                        column: x => x.HandsId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_HeadId",
                        column: x => x.HeadId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_LeftRingId",
                        column: x => x.LeftRingId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_MainHandId",
                        column: x => x.MainHandId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_NeckId",
                        column: x => x.NeckId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_OffHandId",
                        column: x => x.OffHandId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_RightRingId",
                        column: x => x.RightRingId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_ItemInstances_WaistId",
                        column: x => x.WaistId,
                        principalTable: "ItemInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_AttributeTemplateId",
                table: "Attributes",
                column: "AttributeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CreatureId",
                table: "Attributes",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeTemplates_CampaignTemplateId",
                table: "AttributeTemplates",
                column: "CampaignTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPlayers_CampaignId",
                table: "CampaignPlayers",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CampaignTemplateId",
                table: "Campaigns",
                column: "CampaignTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignScenes_CampaignId",
                table: "CampaignScenes",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CreaturePowers_CreatureId",
                table: "CreaturePowers",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CreaturePowers_PowerTemplateId",
                table: "CreaturePowers",
                column: "PowerTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Defenses_CreatureId",
                table: "Defenses",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Defenses_DefenseTemplateId",
                table: "Defenses",
                column: "DefenseTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseTemplates_CampaignTemplateId",
                table: "DefenseTemplates",
                column: "CampaignTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ArmsId",
                table: "Equipment",
                column: "ArmsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ChestId",
                table: "Equipment",
                column: "ChestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CreatureId",
                table: "Equipment",
                column: "CreatureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_FeetId",
                table: "Equipment",
                column: "FeetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_HandsId",
                table: "Equipment",
                column: "HandsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_HeadId",
                table: "Equipment",
                column: "HeadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_LeftRingId",
                table: "Equipment",
                column: "LeftRingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_MainHandId",
                table: "Equipment",
                column: "MainHandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_NeckId",
                table: "Equipment",
                column: "NeckId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_OffHandId",
                table: "Equipment",
                column: "OffHandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_RightRingId",
                table: "Equipment",
                column: "RightRingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_WaistId",
                table: "Equipment",
                column: "WaistId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InboxState_Delivered",
                table: "InboxState",
                column: "Delivered");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CreatureId",
                table: "Inventory",
                column: "CreatureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemConfigurations_CampaignId",
                table: "ItemConfigurations",
                column: "CampaignId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_CreatureId",
                table: "Lifes",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_LifeTemplateId",
                table: "Lifes",
                column: "LifeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeTemplates_CampaignTemplateId",
                table: "LifeTemplates",
                column: "CampaignTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MinorSkills_CreatureId",
                table: "MinorSkills",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_MinorSkills_MinorSkillTemplateId",
                table: "MinorSkills",
                column: "MinorSkillTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MinorSkills_SkillId",
                table: "MinorSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_MinorSkillTemplates_SkillTemplateId",
                table: "MinorSkillTemplates",
                column: "SkillTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                table: "OutboxMessage",
                column: "EnqueueTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                table: "OutboxMessage",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumber",
                table: "OutboxMessage",
                columns: new[] { "InboxMessageId", "InboxConsumerId", "SequenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_OutboxId_SequenceNumber",
                table: "OutboxMessage",
                columns: new[] { "OutboxId", "SequenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_Created",
                table: "OutboxState",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_PowerInstance_TemplateId",
                table: "PowerInstance",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerTemplates_CampaignId",
                table: "PowerTemplates",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Rolls_SceneId",
                table: "Rolls",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_SceneActions_SceneId",
                table: "SceneActions",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_SceneCreatures_SceneId",
                table: "SceneCreatures",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_AttributeId",
                table: "Skills",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CreatureId",
                table: "Skills",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillTemplateId",
                table: "Skills",
                column: "SkillTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillTemplates_AttributeTemplateId",
                table: "SkillTemplates",
                column: "AttributeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillTemplates_CampaignTemplateId",
                table: "SkillTemplates",
                column: "CampaignTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignPlayers");

            migrationBuilder.DropTable(
                name: "CreaturePowers");

            migrationBuilder.DropTable(
                name: "Defenses");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "InboxState");

            migrationBuilder.DropTable(
                name: "ItemConfigurations");

            migrationBuilder.DropTable(
                name: "Lifes");

            migrationBuilder.DropTable(
                name: "MinorSkills");

            migrationBuilder.DropTable(
                name: "OutboxMessage");

            migrationBuilder.DropTable(
                name: "OutboxState");

            migrationBuilder.DropTable(
                name: "PowerInstance");

            migrationBuilder.DropTable(
                name: "Rolls");

            migrationBuilder.DropTable(
                name: "SceneActions");

            migrationBuilder.DropTable(
                name: "SceneCreatures");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DefenseTemplates");

            migrationBuilder.DropTable(
                name: "ItemInstances");

            migrationBuilder.DropTable(
                name: "LifeTemplates");

            migrationBuilder.DropTable(
                name: "MinorSkillTemplates");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "CampaignScenes");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "ItemTemplates");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "SkillTemplates");

            migrationBuilder.DropTable(
                name: "PowerTemplates");

            migrationBuilder.DropTable(
                name: "Creatures");

            migrationBuilder.DropTable(
                name: "AttributeTemplates");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "CampaignTemplates");
        }
    }
}
