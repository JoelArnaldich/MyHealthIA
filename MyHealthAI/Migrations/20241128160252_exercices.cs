using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealthAI.Migrations
{
    /// <inheritdoc />
    public partial class exercices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExerciseHighPerformance",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseLowPerformance",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseMediumPerformance",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "LiftWeights",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Run",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Walk",
                table: "Exercises");

            migrationBuilder.AddColumn<double>(
                name: "CaloriesBurned",
                table: "Exercises",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DurationInMinutes",
                table: "Exercises",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ExerciseType",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesBurned",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseType",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseHighPerformance",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseLowPerformance",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseMediumPerformance",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LiftWeights",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Run",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Walk",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
