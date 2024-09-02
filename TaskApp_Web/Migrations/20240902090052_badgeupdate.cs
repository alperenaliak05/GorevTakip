using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAppWeb.Migrations
{
    /// <inheritdoc />
    public partial class badgeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskCompletionThreshold",
                table: "Badges",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskCompletionThreshold",
                table: "Badges");
        }
    }
}
