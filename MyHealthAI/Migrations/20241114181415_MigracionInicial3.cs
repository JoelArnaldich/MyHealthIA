using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealthAI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalWeight",
                table: "Objectives");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalWeight",
                table: "Objectives",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
