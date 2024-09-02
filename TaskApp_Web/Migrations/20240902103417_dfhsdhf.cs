using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAppWeb.Migrations
{
    /// <inheritdoc />
    public partial class dfhsdhf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedTasksCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedTasksCount",
                table: "Users");
        }
    }
}
