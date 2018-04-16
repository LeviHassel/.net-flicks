using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetFlicks.Accessors.Migrations
{
    public partial class Add_TmdbId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Movies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Genres",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Departments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Departments");
        }
    }
}
