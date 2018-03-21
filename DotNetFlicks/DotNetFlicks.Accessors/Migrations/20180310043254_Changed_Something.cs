using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Migrations
{
    public partial class Changed_Something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMember_Movies_MovieId",
                table: "CastMember");

            migrationBuilder.DropForeignKey(
                name: "FK_CastMember_People_PersonId",
                table: "CastMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CastMember",
                table: "CastMember");

            migrationBuilder.RenameTable(
                name: "CastMember",
                newName: "CastMembers");

            migrationBuilder.RenameIndex(
                name: "IX_CastMember_PersonId",
                table: "CastMembers",
                newName: "IX_CastMembers_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_CastMember_MovieId",
                table: "CastMembers",
                newName: "IX_CastMembers_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CastMembers",
                table: "CastMembers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CastMembers_Movies_MovieId",
                table: "CastMembers",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CastMembers_People_PersonId",
                table: "CastMembers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMembers_Movies_MovieId",
                table: "CastMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_CastMembers_People_PersonId",
                table: "CastMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CastMembers",
                table: "CastMembers");

            migrationBuilder.RenameTable(
                name: "CastMembers",
                newName: "CastMember");

            migrationBuilder.RenameIndex(
                name: "IX_CastMembers_PersonId",
                table: "CastMember",
                newName: "IX_CastMember_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_CastMembers_MovieId",
                table: "CastMember",
                newName: "IX_CastMember_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CastMember",
                table: "CastMember",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CastMember_Movies_MovieId",
                table: "CastMember",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CastMember_People_PersonId",
                table: "CastMember",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
