using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAppWeb.Migrations
{
    /// <inheritdoc />
    public partial class updateupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "TaskReports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UsersId",
                table: "Tasks",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReports_UsersId",
                table: "TaskReports",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskReports_Users_UsersId",
                table: "TaskReports",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UsersId",
                table: "Tasks",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskReports_Users_UsersId",
                table: "TaskReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UsersId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UsersId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_TaskReports_UsersId",
                table: "TaskReports");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "TaskReports");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
