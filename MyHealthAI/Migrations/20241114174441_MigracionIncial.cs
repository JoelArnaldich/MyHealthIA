using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealthAI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionIncial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MealType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoseWeight = table.Column<bool>(type: "bit", nullable: false),
                    LoseWeightWinMuscle = table.Column<bool>(type: "bit", nullable: false),
                    WinMuscleWinWeight = table.Column<bool>(type: "bit", nullable: false),
                    GoalWeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectiveID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Objectives_ObjectiveID",
                        column: x => x.ObjectiveID,
                        principalTable: "Objectives",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AnswerIA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AnswerIA_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DialyExercises",
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
                    WaterLiter = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kcal = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Protein = table.Column<int>(type: "int", nullable: false),
                    Carbohydrate = table.Column<int>(type: "int", nullable: false),
                    Fat = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    MealTypeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Meals_MealType_MealTypeID",
                        column: x => x.MealTypeID,
                        principalTable: "MealType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerIA_UserID",
                table: "AnswerIA",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DialyExercises_UserID",
                table: "DialyExercises",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DialyWater_UserID",
                table: "DialyWater",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealTypeID",
                table: "Meals",
                column: "MealTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserID",
                table: "Meals",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ObjectiveID",
                table: "Users",
                column: "ObjectiveID",
                unique: true,
                filter: "[ObjectiveID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerIA");

            migrationBuilder.DropTable(
                name: "DialyExercises");

            migrationBuilder.DropTable(
                name: "DialyWater");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "MealType");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Objectives");
        }
    }
}
