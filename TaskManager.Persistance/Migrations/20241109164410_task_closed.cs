using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Persistance.Migrations
{
    public partial class task_closed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "TASK",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "TASK");
        }
    }
}
