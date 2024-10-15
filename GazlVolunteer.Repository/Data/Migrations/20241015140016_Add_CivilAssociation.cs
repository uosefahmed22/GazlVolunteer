using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GazlVolunteer.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_CivilAssociation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CivilAssociations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,16)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivilAssociations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CivilAssociations");
        }
    }
}
