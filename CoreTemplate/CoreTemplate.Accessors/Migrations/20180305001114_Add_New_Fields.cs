using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Migrations
{
    public partial class Add_New_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "People",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "People",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrailerUrl",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "People");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TrailerUrl",
                table: "Movies");
        }
    }
}
