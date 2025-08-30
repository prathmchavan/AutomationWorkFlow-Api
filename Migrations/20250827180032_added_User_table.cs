using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowAutomation.Api.Migrations
{
    /// <inheritdoc />
    public partial class added_User_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowSteps_Workflows_WorkflowId",
                table: "WorkflowSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowSteps",
                table: "WorkflowSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflows",
                table: "Workflows");

            migrationBuilder.RenameTable(
                name: "WorkflowSteps",
                newName: "WorkflowStep");

            migrationBuilder.RenameTable(
                name: "Workflows",
                newName: "Workflow");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowSteps_WorkflowId",
                table: "WorkflowStep",
                newName: "IX_WorkflowStep_WorkflowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowStep",
                table: "WorkflowStep",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStep_Workflow_WorkflowId",
                table: "WorkflowStep",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStep_Workflow_WorkflowId",
                table: "WorkflowStep");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowStep",
                table: "WorkflowStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow");

            migrationBuilder.RenameTable(
                name: "WorkflowStep",
                newName: "WorkflowSteps");

            migrationBuilder.RenameTable(
                name: "Workflow",
                newName: "Workflows");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowStep_WorkflowId",
                table: "WorkflowSteps",
                newName: "IX_WorkflowSteps_WorkflowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowSteps",
                table: "WorkflowSteps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflows",
                table: "Workflows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowSteps_Workflows_WorkflowId",
                table: "WorkflowSteps",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
