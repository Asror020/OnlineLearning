using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLearning.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_AssignmentId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Students_AssignedToId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssignedToId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssignmentId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Assignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedToId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignedToId",
                table: "Assignments",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssignmentId",
                table: "Assignments",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_AssignmentId",
                table: "Assignments",
                column: "AssignmentId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Students_AssignedToId",
                table: "Assignments",
                column: "AssignedToId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
