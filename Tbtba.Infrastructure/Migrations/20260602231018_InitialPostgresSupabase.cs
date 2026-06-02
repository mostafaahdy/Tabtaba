using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tabtaba.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresSupabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           //   migrationBuilder.EnsureSchema(
               // name: "public");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    EntityName = table.Column<string>(type: "text", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonConditions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Condition_Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCards",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardHolderName = table.Column<string>(type: "text", nullable: false),
                    MaskedCardNumber = table.Column<string>(type: "text", nullable: false),
                    ExpiryDate = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    BillingCycle = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "relaxContents",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    MediaUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relaxContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_claims_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyMessages",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message_Text = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Date_Shown = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMessages_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Specialization = table.Column<string>(type: "text", nullable: false),
                    License_Number = table.Column<string>(type: "text", nullable: false),
                    Years_Experience = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    Availability_Status = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KidsZones",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Media_Path = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KidsZones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KidsZones_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    SenderId = table.Column<string>(type: "text", nullable: false),
                    ReceiverId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MoodLogs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PatientId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoodLogs_users_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaritalStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ClientRole = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MedicalHistorySummary = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    RemainingSessions = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    EnableNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    MoodTrackingReminders = table.Column<bool>(type: "boolean", nullable: false),
                    DarkMode = table.Column<bool>(type: "boolean", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    SessionReminders = table.Column<bool>(type: "boolean", nullable: false),
                    MoodTrackingUpdates = table.Column<bool>(type: "boolean", nullable: false),
                    TherapistMessages = table.Column<bool>(type: "boolean", nullable: false),
                    NewContentAlerts = table.Column<bool>(type: "boolean", nullable: false),
                    PersonalizedTips = table.Column<bool>(type: "boolean", nullable: false),
                    ReceiveEmails = table.Column<bool>(type: "boolean", nullable: false),
                    ReceiveNotifications = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patients_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "relaxLogs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<string>(type: "text", nullable: false),
                    RelaxContentId = table.Column<int>(type: "integer", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relaxLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_relaxLogs_relaxContents_RelaxContentId",
                        column: x => x.RelaxContentId,
                        principalSchema: "public",
                        principalTable: "relaxContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_relaxLogs_users_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "therapists",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<int>(type: "integer", nullable: true),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true),
                    CountryOfResidence = table.Column<string>(type: "text", nullable: true),
                    CvUrl = table.Column<string>(type: "text", nullable: true),
                    Specialization = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    YearsOfExperience = table.Column<int>(type: "integer", nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "text", nullable: true),
                    IntroAudioUrl = table.Column<string>(type: "text", nullable: false),
                    AboutDescription = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_therapists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_therapists_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_claims_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                schema: "public",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_user_logins_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_user_tokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeLibraries",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    ReadTimeMinutes = table.Column<int>(type: "integer", nullable: false),
                    VideoUrl = table.Column<string>(type: "text", nullable: true),
                    ViewsCount = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeLibraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeLibraries_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "public",
                        principalTable: "Doctor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Withdrawals",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdrawals_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "public",
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Progress_Level = table.Column<int>(type: "integer", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    PatientId1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Achievements_patients_PatientId1",
                        column: x => x.PatientId1,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date_Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Session_Type = table.Column<string>(type: "text", nullable: false),
                    Duration_Minutes = table.Column<int>(type: "integer", nullable: false),
                    Location_Mode = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Zoom_Meeting_Url = table.Column<string>(type: "text", nullable: true),
                    Zoom_Meeting_Id = table.Column<string>(type: "text", nullable: true),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "public",
                        principalTable: "Doctor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatLogs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    UserMessage = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    BotResponse = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    AudioPath = table.Column<string>(type: "text", nullable: true),
                    MessageType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PatientId1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatLogs_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatLogs_patients_PatientId1",
                        column: x => x.PatientId1,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Entry_Text = table.Column<string>(type: "text", nullable: false),
                    Voice_Note_Path = table.Column<string>(type: "text", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Creation_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Last_Updated_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgressTrackers",
                schema: "public",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date_Recorded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Mood_Score = table.Column<int>(type: "integer", nullable: false),
                    Anxiety_Level = table.Column<int>(type: "integer", nullable: false),
                    Sleep_Hours = table.Column<double>(type: "double precision", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressTrackers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgressTrackers_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "public",
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapistAvailabilities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorksAtClinic = table.Column<bool>(type: "boolean", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    FromTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ToTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistAvailabilities_therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalSchema: "public",
                        principalTable: "therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapistDocuments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentType = table.Column<int>(type: "integer", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    OriginalFileName = table.Column<string>(type: "text", nullable: true),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistDocuments_therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalSchema: "public",
                        principalTable: "therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapistEducations",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uuid", nullable: false),
                    HighestDegree = table.Column<string>(type: "text", nullable: false),
                    GraduationYear = table.Column<int>(type: "integer", nullable: false),
                    UniversityName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistEducations_therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalSchema: "public",
                        principalTable: "therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapistLanguages",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uuid", nullable: false),
                    Language = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistLanguages_therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalSchema: "public",
                        principalTable: "therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapistProfessionalInfos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessionalCategory = table.Column<string>(type: "text", nullable: false),
                    Specialization = table.Column<string>(type: "text", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "integer", nullable: false),
                    LicensingAuthority = table.Column<string>(type: "text", nullable: true),
                    LicenseNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistProfessionalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistProfessionalInfos_therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalSchema: "public",
                        principalTable: "therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeComments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    KnowledgeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeComments_KnowledgeLibraries_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalSchema: "public",
                        principalTable: "KnowledgeLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KnowledgeComments_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeLikes",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    KnowledgeId = table.Column<int>(type: "integer", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeLikes", x => new { x.UserId, x.KnowledgeId });
                    table.ForeignKey(
                        name: "FK_KnowledgeLikes_KnowledgeLibraries_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalSchema: "public",
                        principalTable: "KnowledgeLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSavedContents",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    KnowledgeLibraryId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    KnowledgeLibraryId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSavedContents", x => new { x.UserId, x.KnowledgeLibraryId });
                    table.ForeignKey(
                        name: "FK_UserSavedContents_KnowledgeLibraries_KnowledgeLibraryId",
                        column: x => x.KnowledgeLibraryId,
                        principalSchema: "public",
                        principalTable: "KnowledgeLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSavedContents_KnowledgeLibraries_KnowledgeLibraryId1",
                        column: x => x.KnowledgeLibraryId1,
                        principalSchema: "public",
                        principalTable: "KnowledgeLibraries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSavedContents_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoChapters",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Timestamp = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    KnowledgeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoChapters_KnowledgeLibraries_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalSchema: "public",
                        principalTable: "KnowledgeLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient_Appointment_Doctors",
                schema: "public",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Appointment_Doctors", x => new { x.PatientId, x.AppointmentId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_Patient_Appointment_Doctors_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "public",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Appointment_Doctors_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "public",
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Appointment_Doctors_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    WalletPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ServiceFee = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "public",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false),
                    Rating_Score = table.Column<int>(type: "integer", nullable: false),
                    Review_Text = table.Column<string>(type: "text", nullable: false),
                    Patient_Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    TherapistId = table.Column<int>(type: "integer", nullable: false),
                    TherapistId1 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "public",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "public",
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "public",
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_therapists_TherapistId1",
                        column: x => x.TherapistId1,
                        principalSchema: "public",
                        principalTable: "therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionNotes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Last_Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionNotes_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "public",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Diagnosis_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Diagnosis_Details = table.Column<string>(type: "text", nullable: false),
                    RecordID = table.Column<int>(type: "integer", nullable: false),
                    ConditionId = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    MedicalRecordId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalSchema: "public",
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "public",
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnoses_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalSchema: "public",
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonConditions_Diagnoses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommonCondition_ID = table.Column<int>(type: "integer", nullable: false),
                    DiagnosisId = table.Column<int>(type: "integer", nullable: false),
                    CommonConditionId = table.Column<int>(type: "integer", nullable: false),
                    ConditionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonConditions_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonConditions_Diagnoses_CommonConditions_CommonCondition~",
                        column: x => x.CommonConditionId,
                        principalSchema: "public",
                        principalTable: "CommonConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommonConditions_Diagnoses_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalSchema: "public",
                        principalTable: "Conditions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommonConditions_Diagnoses_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalSchema: "public",
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
     schema: "public",
     table: "KnowledgeLibraries",
     columns: new[] { "Id", "Category", "ContentType", "CreatedAt", "Description", "DoctorId", "ImageUrl", "ReadTimeMinutes", "Title", "VideoUrl", "ViewsCount" },
     values: new object[,]
     {
        { 1, "ADHD", "Article", new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), "ADHD is not just about focus", null, null, 0, "Understanding ADHD", null, 0L },
        { 2, "Autism", "Article", new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Autism is a spectrum of strengths", null, null, 0, "Living with Autism", null, 0L },
        { 3, "OCD", "Article", new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Obsessive-Compulsive Disorder (OCD) is more than just a need for cleanliness. It is a mental health condition characterized by intrusive thoughts (obsessions) and repetitive behaviors (compulsions) that can significantly impact daily life.", null, null, 0, "Understanding OCD", null, 0L },
        { 4, "Alzheimer", "Video", new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Alzheimer's is a progressive brain disorder that affects memory, thinking, and behavior. Early detection and providing a supportive environment are crucial steps in managing the journey for both patients and their families.", null, null, 0, "A Guide to Alzheimer's", null, 0L }
     });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_PatientId",
                schema: "public",
                table: "Achievements",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_PatientId1",
                schema: "public",
                table: "Achievements",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                schema: "public",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                schema: "public",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatLogs_PatientId",
                schema: "public",
                table: "ChatLogs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatLogs_PatientId1",
                schema: "public",
                table: "ChatLogs",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_CommonConditions_Diagnoses_CommonConditionId",
                schema: "public",
                table: "CommonConditions_Diagnoses",
                column: "CommonConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonConditions_Diagnoses_ConditionId",
                schema: "public",
                table: "CommonConditions_Diagnoses",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonConditions_Diagnoses_DiagnosisId",
                schema: "public",
                table: "CommonConditions_Diagnoses",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMessages_UserId",
                schema: "public",
                table: "DailyMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_ConditionId",
                schema: "public",
                table: "Diagnoses",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_DoctorId",
                schema: "public",
                table: "Diagnoses",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_MedicalRecordId",
                schema: "public",
                table: "Diagnoses",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UserId",
                schema: "public",
                table: "Doctor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_PatientId",
                schema: "public",
                table: "Journals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_KidsZones_UserId",
                schema: "public",
                table: "KidsZones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeComments_KnowledgeId",
                schema: "public",
                table: "KnowledgeComments",
                column: "KnowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeComments_UserId",
                schema: "public",
                table: "KnowledgeComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeLibraries_DoctorId",
                schema: "public",
                table: "KnowledgeLibraries",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeLikes_KnowledgeId",
                schema: "public",
                table: "KnowledgeLikes",
                column: "KnowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                schema: "public",
                table: "MedicalRecords",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                schema: "public",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                schema: "public",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MoodLogs_PatientId",
                schema: "public",
                table: "MoodLogs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Appointment_Doctors_AppointmentId",
                schema: "public",
                table: "Patient_Appointment_Doctors",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Appointment_Doctors_DoctorId",
                schema: "public",
                table: "Patient_Appointment_Doctors",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_UserId",
                schema: "public",
                table: "patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppointmentId",
                schema: "public",
                table: "Payments",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PatientId",
                schema: "public",
                table: "Payments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressTrackers_PatientId",
                schema: "public",
                table: "ProgressTrackers",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_relaxLogs_PatientId",
                schema: "public",
                table: "relaxLogs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_relaxLogs_RelaxContentId",
                schema: "public",
                table: "relaxLogs",
                column: "RelaxContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AppointmentId",
                schema: "public",
                table: "Reviews",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DoctorId",
                schema: "public",
                table: "Reviews",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PatientId",
                schema: "public",
                table: "Reviews",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TherapistId1",
                schema: "public",
                table: "Reviews",
                column: "TherapistId1");

            migrationBuilder.CreateIndex(
                name: "IX_role_claims_RoleId",
                schema: "public",
                table: "role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionNotes_AppointmentId",
                schema: "public",
                table: "SessionNotes",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PatientId",
                schema: "public",
                table: "Subscriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                schema: "public",
                table: "Subscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistAvailabilities_TherapistId",
                schema: "public",
                table: "TherapistAvailabilities",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistDocuments_TherapistId",
                schema: "public",
                table: "TherapistDocuments",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistEducations_TherapistId",
                schema: "public",
                table: "TherapistEducations",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistLanguages_TherapistId",
                schema: "public",
                table: "TherapistLanguages",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistProfessionalInfos_TherapistId",
                schema: "public",
                table: "TherapistProfessionalInfos",
                column: "TherapistId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_therapists_UserId",
                schema: "public",
                table: "therapists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_claims_UserId",
                schema: "public",
                table: "user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_logins_UserId",
                schema: "public",
                table: "user_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_RoleId",
                schema: "public",
                table: "user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSavedContents_KnowledgeLibraryId",
                schema: "public",
                table: "UserSavedContents",
                column: "KnowledgeLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSavedContents_KnowledgeLibraryId1",
                schema: "public",
                table: "UserSavedContents",
                column: "KnowledgeLibraryId1");

            migrationBuilder.CreateIndex(
                name: "IX_VideoChapters_KnowledgeId",
                schema: "public",
                table: "VideoChapters",
                column: "KnowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_DoctorId",
                schema: "public",
                table: "Withdrawals",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ChatLogs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CommonConditions_Diagnoses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DailyMessages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Journals",
                schema: "public");

            migrationBuilder.DropTable(
                name: "KidsZones",
                schema: "public");

            migrationBuilder.DropTable(
                name: "KnowledgeComments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "KnowledgeLikes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MoodLogs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Patient_Appointment_Doctors",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PaymentCards",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProgressTrackers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "relaxLogs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "public");

            migrationBuilder.DropTable(
                name: "role_claims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SessionNotes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TherapistAvailabilities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TherapistDocuments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TherapistEducations",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TherapistLanguages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TherapistProfessionalInfos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_claims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_logins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_tokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserSavedContents",
                schema: "public");

            migrationBuilder.DropTable(
                name: "VideoChapters",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Withdrawals",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CommonConditions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Diagnoses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "relaxContents",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Plans",
                schema: "public");

            migrationBuilder.DropTable(
                name: "therapists",
                schema: "public");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "KnowledgeLibraries",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Conditions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MedicalRecords",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Doctor",
                schema: "public");

            migrationBuilder.DropTable(
                name: "patients",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");
        }
    }
}
