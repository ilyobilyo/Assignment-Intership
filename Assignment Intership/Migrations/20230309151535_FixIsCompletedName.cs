using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_Intership.Migrations
{
    public partial class FixIsCompletedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsComleted",
                table: "Tasks",
                newName: "IsCompleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "Tasks",
                newName: "IsComleted");
        }
    }
}
