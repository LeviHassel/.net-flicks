using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreTemplate.Accessors.Migrations
{
    public partial class Add_Movies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Runtime = table.Column<int>(type: "int", nullable: false),
                Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
