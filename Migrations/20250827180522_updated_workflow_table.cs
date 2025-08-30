using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowAutomation.Api.Migrations
{
    /// <inheritdoc />
    public partial class updated_workflow_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Workflow",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_UserId",
                table: "Workflow",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_UserId",
                table: "Workflow",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_UserId",
                table: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_Workflow_UserId",
                table: "Workflow");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workflow");
        }
    }
}
