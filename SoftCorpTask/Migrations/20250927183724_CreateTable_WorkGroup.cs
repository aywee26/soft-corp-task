using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftCorpTask.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable_WorkGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkGroupId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkGroupId",
                table: "Users",
                column: "WorkGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WorkGroups_WorkGroupId",
                table: "Users",
                column: "WorkGroupId",
                principalTable: "WorkGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_WorkGroups_WorkGroupId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "WorkGroups");

            migrationBuilder.DropIndex(
                name: "IX_Users_WorkGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkGroupId",
                table: "Users");
        }
    }
}
