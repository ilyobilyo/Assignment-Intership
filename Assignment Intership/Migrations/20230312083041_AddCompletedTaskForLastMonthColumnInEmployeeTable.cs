using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_Intership.Migrations
{
    public partial class AddCompletedTaskForLastMonthColumnInEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedTasksForLastMonth",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedTasksForLastMonth",
                table: "Employees");
        }
    }
}
