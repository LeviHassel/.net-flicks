using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Migrations
{
    public partial class Rename_Job_Title_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePersons_JobTitles_JobTitleId",
                table: "MoviePersons");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.RenameColumn(
                name: "JobTitleId",
                table: "MoviePersons",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePersons_JobTitleId",
                table: "MoviePersons",
                newName: "IX_MoviePersons_JobId");

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePersons_Jobs_JobId",
                table: "MoviePersons",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePersons_Jobs_JobId",
                table: "MoviePersons");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "MoviePersons",
                newName: "JobTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePersons_JobId",
                table: "MoviePersons",
                newName: "IX_MoviePersons_JobTitleId");

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePersons_JobTitles_JobTitleId",
                table: "MoviePersons",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
