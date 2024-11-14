using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealthAI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionIncial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lift_Weights",
                table: "DialyExercises",
                newName: "LiftWeights");

            migrationBuilder.AddColumn<int>(
                name: "GoalWeight",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalWeight",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LiftWeights",
                table: "DialyExercises",
                newName: "Lift_Weights");
        }
    }
}
