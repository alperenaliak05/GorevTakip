using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskAppWeb.Migrations
{
    public partial class AddProfilePictureToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            // TaskReports tablosu yaratma işlemi zaten var mı kontrol edin, eğer varsa eklemeyin.
            if (!migrationBuilder.ActiveProvider.Contains("SqlServer"))
            {
                migrationBuilder.CreateTable(
                    name: "TaskReports",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        TaskId = table.Column<int>(type: "int", nullable: false),
                        Report = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_TaskReports", x => x.Id);
                        table.ForeignKey(
                            name: "FK_TaskReports_Tasks_TaskId",
                            column: x => x.TaskId,
                            principalTable: "Tasks",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_TaskReports_Users_CreatedByUserId",
                            column: x => x.CreatedByUserId,
                            principalTable: "Users",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_TaskReports_CreatedByUserId",
                    table: "TaskReports",
                    column: "CreatedByUserId");

                migrationBuilder.CreateIndex(
                    name: "IX_TaskReports_TaskId",
                    table: "TaskReports",
                    column: "TaskId");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskReports");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");
        }
    }
}
