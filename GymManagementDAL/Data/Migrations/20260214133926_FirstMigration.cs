using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "MemberShips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_SessionId",
                table: "MemberShips",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShips_Sessions_SessionId",
                table: "MemberShips",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShips_Sessions_SessionId",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_SessionId",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "MemberShips");
        }
    }
}
