using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealthAI.Migrations
{
    /// <inheritdoc />
    public partial class Water2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialyExercises");

            migrationBuilder.DropTable(
                name: "DialyWater");

            migrationBuilder.AddColumn<int>(
                name: "DailyWater",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Walk = table.Column<int>(type: "int", nullable: false),
                    Run = table.Column<int>(type: "int", nullable: false),
                    LiftWeights = table.Column<int>(type: "int", nullable: false),
                    ExerciseHighPerformance = table.Column<int>(type: "int", nullable: false),
                    ExerciseMediumPerformance = table.Column<int>(type: "int", nullable: false),
                    ExerciseLowPerformance = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Exercises_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Water",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WaterMl = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Water", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Water_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_UserID",
                table: "Exercises",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Water_UserID",
                table: "Water",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Water");

            migrationBuilder.DropColumn(
                name: "DailyWater",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "DialyExercises",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ExerciseHighPerformance = table.Column<int>(type: "int", nullable: false),
                    ExerciseLowPerformance = table.Column<int>(type: "int", nullable: false),
                    ExerciseMediumPerformance = table.Column<int>(type: "int", nullable: false),
                    LiftWeights = table.Column<int>(type: "int", nullable: false),
                    Run = table.Column<int>(type: "int", nullable: false),
                    Walk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialyExercises", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DialyExercises_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DialyWater",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    WaterLiter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialyWater", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DialyWater_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DialyExercises_UserID",
                table: "DialyExercises",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DialyWater_UserID",
                table: "DialyWater",
                column: "UserID");
        }
    }
}
