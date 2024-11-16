using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHealthAI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailtoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_ObjectiveID",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_ObjectiveID",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "ObjectiveID",
                table: "Objectives");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ObjectiveID",
                table: "Users",
                column: "ObjectiveID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Objectives_ObjectiveID",
                table: "Users",
                column: "ObjectiveID",
                principalTable: "Objectives",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Objectives_ObjectiveID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ObjectiveID",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ObjectiveID",
                table: "Objectives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_ObjectiveID",
                table: "Objectives",
                column: "ObjectiveID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_ObjectiveID",
                table: "Objectives",
                column: "ObjectiveID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
