using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RemoveUserConditionFromDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conditions_Users_UserId",
                table: "Conditions");

            migrationBuilder.DropIndex(
                name: "IX_Conditions_UserId",
                table: "Conditions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Conditions_UserId",
                table: "Conditions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditions_Users_UserId",
                table: "Conditions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
