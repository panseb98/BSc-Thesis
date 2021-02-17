using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Migrations
{
    public partial class _11111232 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_CONCURRENCY_STAMP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    USER_MODIFY_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USER_PW_HASH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_PHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_SECURITY_STAMP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_USER_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_EDUCATION",
                columns: table => new
                {
                    UED_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UED_END_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UED_FIELD_OF_STUDY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UED_IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    UED_IS_NOW = table.Column<bool>(type: "bit", nullable: false),
                    UED_START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UED_STUDY_LEVEL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UED_UNIVERSITY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UED_USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_EDUCATION", x => x.UED_ID);
                    table.ForeignKey(
                        name: "FK_USER_EDUCATION_USER_UED_USER_ID",
                        column: x => x.UED_USER_ID,
                        principalTable: "USER",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_EXPERIENCE",
                columns: table => new
                {
                    UEX_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UEX_COMPANY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UEX_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UEX_END_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UEX_IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    UEX_IS_NOW = table.Column<bool>(type: "bit", nullable: false),
                    UEX_POSITION_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UEX_START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UEX_USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_EXPERIENCE", x => x.UEX_ID);
                    table.ForeignKey(
                        name: "FK_USER_EXPERIENCE_USER_UEX_USER_ID",
                        column: x => x.UEX_USER_ID,
                        principalTable: "USER",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_PERSONAL_DATA",
                columns: table => new
                {
                    UPD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UPD_FIRST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPD_LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPD_USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_PERSONAL_DATA", x => x.UPD_ID);
                    table.ForeignKey(
                        name: "FK_USER_PERSONAL_DATA_USER_UPD_USER_ID",
                        column: x => x.UPD_USER_ID,
                        principalTable: "USER",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_SKILL",
                columns: table => new
                {
                    US_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    US_SKILL_ID = table.Column<int>(type: "int", nullable: false),
                    US_USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_SKILL", x => x.US_ID);
                    table.ForeignKey(
                        name: "FK_USER_SKILL_JOB_SKILL_US_SKILL_ID",
                        column: x => x.US_SKILL_ID,
                        principalTable: "JOB_SKILL",
                        principalColumn: "JSL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_SKILL_USER_US_USER_ID",
                        column: x => x.US_USER_ID,
                        principalTable: "USER",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_EDUCATION_UED_USER_ID",
                table: "USER_EDUCATION",
                column: "UED_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_EXPERIENCE_UEX_USER_ID",
                table: "USER_EXPERIENCE",
                column: "UEX_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_PERSONAL_DATA_UPD_USER_ID",
                table: "USER_PERSONAL_DATA",
                column: "UPD_USER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_SKILL_US_SKILL_ID",
                table: "USER_SKILL",
                column: "US_SKILL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SKILL_US_USER_ID",
                table: "USER_SKILL",
                column: "US_USER_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
