using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Migrations
{
    public partial class Rename_Persons_To_People : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePersons_Jobs_JobId",
                table: "MoviePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePersons_Movies_MovieId",
                table: "MoviePersons");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePersons_Persons_PersonId",
                table: "MoviePersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePersons",
                table: "MoviePersons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "People");

            migrationBuilder.RenameTable(
                name: "MoviePersons",
                newName: "MoviePeople");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePersons_PersonId",
                table: "MoviePeople",
                newName: "IX_MoviePeople_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePersons_MovieId",
                table: "MoviePeople",
                newName: "IX_MoviePeople_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePersons_JobId",
                table: "MoviePeople",
                newName: "IX_MoviePeople_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePeople",
                table: "MoviePeople",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Jobs_JobId",
                table: "MoviePeople",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_Movies_MovieId",
                table: "MoviePeople",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePeople_People_PersonId",
                table: "MoviePeople",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Jobs_JobId",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_Movies_MovieId",
                table: "MoviePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePeople_People_PersonId",
                table: "MoviePeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePeople",
                table: "MoviePeople");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "MoviePeople",
                newName: "MoviePersons");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePeople_PersonId",
                table: "MoviePersons",
                newName: "IX_MoviePersons_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePeople_MovieId",
                table: "MoviePersons",
                newName: "IX_MoviePersons_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePeople_JobId",
                table: "MoviePersons",
                newName: "IX_MoviePersons_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePersons",
                table: "MoviePersons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePersons_Jobs_JobId",
                table: "MoviePersons",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePersons_Movies_MovieId",
                table: "MoviePersons",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePersons_Persons_PersonId",
                table: "MoviePersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
