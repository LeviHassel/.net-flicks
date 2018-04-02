using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Migrations
{
    public partial class Change_Screentime_to_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenTime",
                table: "CastMembers");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CastMembers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "CastMembers");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ScreenTime",
                table: "CastMembers",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
