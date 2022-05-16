using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes_app.Migrations
{
    public partial class ChangeOfModelProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modificationDate",
                table: "Notes",
                newName: "ModificationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModificationDate",
                table: "Notes",
                newName: "modificationDate");
        }
    }
}
