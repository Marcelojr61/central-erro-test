using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErros.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "environment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phase = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_environment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "level",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "microservice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    token = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_microservice", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profile",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "error",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    title = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    level_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_error", x => x.id);
                    table.ForeignKey(
                        name: "FK_error_level_level_id",
                        column: x => x.level_id,
                        principalTable: "level",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "occurrence",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    origin = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    details = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    occurrence_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    MicrosserviceId = table.Column<int>(nullable: false),
                    EnviromentId = table.Column<int>(nullable: false),
                    ErrorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_occurrence", x => x.id);
                    table.ForeignKey(
                        name: "FK_occurrence_environment_EnviromentId",
                        column: x => x.EnviromentId,
                        principalTable: "environment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_occurrence_error_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "error",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_occurrence_microservice_MicrosserviceId",
                        column: x => x.MicrosserviceId,
                        principalTable: "microservice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_error_level_id",
                table: "error",
                column: "level_id");

            migrationBuilder.CreateIndex(
                name: "IX_occurrence_EnviromentId",
                table: "occurrence",
                column: "EnviromentId");

            migrationBuilder.CreateIndex(
                name: "IX_occurrence_ErrorId",
                table: "occurrence",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_occurrence_MicrosserviceId",
                table: "occurrence",
                column: "MicrosserviceId");

            migrationBuilder.CreateIndex(
                name: "IX_user_ProfileId",
                table: "user",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "occurrence");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "environment");

            migrationBuilder.DropTable(
                name: "error");

            migrationBuilder.DropTable(
                name: "microservice");

            migrationBuilder.DropTable(
                name: "profile");

            migrationBuilder.DropTable(
                name: "level");
        }
    }
}
