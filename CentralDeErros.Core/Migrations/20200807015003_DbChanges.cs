using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErros.API.Migrations
{
    public partial class DbChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_error_level_level_id",
                table: "error");

            migrationBuilder.DropTable(
                name: "occurrence");

            migrationBuilder.DropColumn(
                name: "description",
                table: "error");

            migrationBuilder.RenameColumn(
                name: "level_id",
                table: "error",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_error_level_id",
                table: "error",
                newName: "IX_error_LevelId");

            migrationBuilder.AddColumn<string>(
                name: "details",
                table: "error",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EnviromentId",
                table: "error",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "error_date",
                table: "error",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MicrosserviceId",
                table: "error",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "origin",
                table: "error",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_error_EnviromentId",
                table: "error",
                column: "EnviromentId");

            migrationBuilder.CreateIndex(
                name: "IX_error_MicrosserviceId",
                table: "error",
                column: "MicrosserviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_error_environment_EnviromentId",
                table: "error",
                column: "EnviromentId",
                principalTable: "environment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_error_level_LevelId",
                table: "error",
                column: "LevelId",
                principalTable: "level",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_error_microservice_MicrosserviceId",
                table: "error",
                column: "MicrosserviceId",
                principalTable: "microservice",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_error_environment_EnviromentId",
                table: "error");

            migrationBuilder.DropForeignKey(
                name: "FK_error_level_LevelId",
                table: "error");

            migrationBuilder.DropForeignKey(
                name: "FK_error_microservice_MicrosserviceId",
                table: "error");

            migrationBuilder.DropIndex(
                name: "IX_error_EnviromentId",
                table: "error");

            migrationBuilder.DropIndex(
                name: "IX_error_MicrosserviceId",
                table: "error");

            migrationBuilder.DropColumn(
                name: "details",
                table: "error");

            migrationBuilder.DropColumn(
                name: "EnviromentId",
                table: "error");

            migrationBuilder.DropColumn(
                name: "error_date",
                table: "error");

            migrationBuilder.DropColumn(
                name: "MicrosserviceId",
                table: "error");

            migrationBuilder.DropColumn(
                name: "origin",
                table: "error");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "error",
                newName: "level_id");

            migrationBuilder.RenameIndex(
                name: "IX_error_LevelId",
                table: "error",
                newName: "IX_error_level_id");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "error",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "occurrence",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    details = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    EnviromentId = table.Column<int>(type: "int", nullable: false),
                    ErrorId = table.Column<int>(type: "int", nullable: false),
                    MicrosserviceId = table.Column<int>(type: "int", nullable: false),
                    occurrence_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    origin = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_error_level_level_id",
                table: "error",
                column: "level_id",
                principalTable: "level",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
