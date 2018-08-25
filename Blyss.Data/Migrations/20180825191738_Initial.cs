namespace Blyss.Data.Migrations
{

    using System;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Initial : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AspNetRoles",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 15, nullable: true),
                    LastName = table.Column<string>(maxLength: 15, nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 45, nullable: true),
                    ProfilePicture = table.Column<byte[]>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Categories",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });

            migrationBuilder.CreateTable(
                "Languages",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Languages", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);

                    table.ForeignKey(
                        "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);

                    table.ForeignKey(
                        "FK_AspNetUserClaims_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserLogins",
                table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new {x.LoginProvider, x.ProviderKey});

                    table.ForeignKey(
                        "FK_AspNetUserLogins_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserRoles",
                table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new {x.UserId, x.RoleId});

                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserTokens",
                table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new {x.UserId, x.LoginProvider, x.Name});

                    table.ForeignKey(
                        "FK_AspNetUserTokens_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Courses",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 13, nullable: false),
                    Description = table.Column<string>(maxLength: 45, nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<string>(nullable: true),
                    VideoId = table.Column<string>(nullable: true),
                    LanguageId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);

                    table.ForeignKey(
                        "FK_Courses_Categories_CategoryId",
                        x => x.CategoryId,
                        "Categories",
                        "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        "FK_Courses_Languages_LanguageId",
                        x => x.LanguageId,
                        "Languages",
                        "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        "FK_Courses_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Videos",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    YouTubeId = table.Column<string>(nullable: false),
                    PlainYoutubeId = table.Column<string>(maxLength: 11, nullable: false),
                    CourseId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);

                    table.ForeignKey(
                        "FK_Videos_Courses_CourseId",
                        x => x.CourseId,
                        "Courses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_AspNetRoleClaims_RoleId",
                "AspNetRoleClaims",
                "RoleId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserClaims_UserId",
                "AspNetUserClaims",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserLogins_UserId",
                "AspNetUserLogins",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_RoleId",
                "AspNetUserRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_Email",
                "AspNetUsers",
                "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "AspNetUsers",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "AspNetUsers",
                "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_UserName",
                "AspNetUsers",
                "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Categories_Name",
                "Categories",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Courses_CategoryId",
                "Courses",
                "CategoryId");

            migrationBuilder.CreateIndex(
                "IX_Courses_LanguageId",
                "Courses",
                "LanguageId");

            migrationBuilder.CreateIndex(
                "IX_Courses_Name",
                "Courses",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Courses_UserId",
                "Courses",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Languages_Name",
                "Languages",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Videos_CourseId",
                "Videos",
                "CourseId",
                unique: true,
                filter: "[CourseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Videos_PlainYoutubeId",
                "Videos",
                "PlainYoutubeId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Videos_YouTubeId",
                "Videos",
                "YouTubeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AspNetRoleClaims");

            migrationBuilder.DropTable(
                "AspNetUserClaims");

            migrationBuilder.DropTable(
                "AspNetUserLogins");

            migrationBuilder.DropTable(
                "AspNetUserRoles");

            migrationBuilder.DropTable(
                "AspNetUserTokens");

            migrationBuilder.DropTable(
                "Videos");

            migrationBuilder.DropTable(
                "AspNetRoles");

            migrationBuilder.DropTable(
                "Courses");

            migrationBuilder.DropTable(
                "Categories");

            migrationBuilder.DropTable(
                "Languages");

            migrationBuilder.DropTable(
                "AspNetUsers");
        }

    }

}