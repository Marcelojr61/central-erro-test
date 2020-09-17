using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralDeErros.API.Migrations
{
    public partial class microsservicerefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_error_microservice_MicrosserviceId",
                table: "error");

            migrationBuilder.DropPrimaryKey(
                name: "PK_microservice",
                table: "microservice");

            migrationBuilder.DropIndex(
                name: "IX_error_MicrosserviceId",
                table: "error");

            migrationBuilder.DropColumn(
                name: "id",
                table: "microservice");

            migrationBuilder.DropColumn(
                name: "token",
                table: "microservice");

            migrationBuilder.DropColumn(
                name: "MicrosserviceId",
                table: "error");

            migrationBuilder.AddColumn<string>(
                name: "client_id",
                table: "microservice",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "client_secret",
                table: "microservice",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MicrosserviceClientId",
                table: "error",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_microservice",
                table: "microservice",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_error_MicrosserviceClientId",
                table: "error",
                column: "MicrosserviceClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_error_microservice_MicrosserviceClientId",
                table: "error",
                column: "MicrosserviceClientId",
                principalTable: "microservice",
                principalColumn: "client_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_error_microservice_MicrosserviceClientId",
                table: "error");

            migrationBuilder.DropPrimaryKey(
                name: "PK_microservice",
                table: "microservice");

            migrationBuilder.DropIndex(
                name: "IX_error_MicrosserviceClientId",
                table: "error");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "microservice");

            migrationBuilder.DropColumn(
                name: "client_secret",
                table: "microservice");

            migrationBuilder.DropColumn(
                name: "MicrosserviceClientId",
                table: "error");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "microservice",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "microservice",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MicrosserviceId",
                table: "error",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_microservice",
                table: "microservice",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_error_MicrosserviceId",
                table: "error",
                column: "MicrosserviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_error_microservice_MicrosserviceId",
                table: "error",
                column: "MicrosserviceId",
                principalTable: "microservice",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
