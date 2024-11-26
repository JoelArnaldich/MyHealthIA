using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealthAI.Migrations
{
    /// <inheritdoc />
    public partial class pro5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DialyPro",
                table: "Users",
                newName: "DailyPro");

            migrationBuilder.RenameColumn(
                name: "DialyKcal",
                table: "Users",
                newName: "DailyKcal");

            migrationBuilder.RenameColumn(
                name: "DialyFat",
                table: "Users",
                newName: "DailyFat");

            migrationBuilder.RenameColumn(
                name: "DialyCar",
                table: "Users",
                newName: "DailyCar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DailyPro",
                table: "Users",
                newName: "DialyPro");

            migrationBuilder.RenameColumn(
                name: "DailyKcal",
                table: "Users",
                newName: "DialyKcal");

            migrationBuilder.RenameColumn(
                name: "DailyFat",
                table: "Users",
                newName: "DialyFat");

            migrationBuilder.RenameColumn(
                name: "DailyCar",
                table: "Users",
                newName: "DialyCar");
        }
    }
}
