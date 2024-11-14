using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealthAI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionIncial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LiftWeights",
                table: "DialyExercises",
                newName: "Lift_Weights");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lift_Weights",
                table: "DialyExercises",
                newName: "LiftWeights");
        }
    }
}
