using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class fixEmployeeJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppEmployee_JobID",
                table: "AppEmployee",
                column: "JobID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEmployee_AppJob_JobID",
                table: "AppEmployee",
                column: "JobID",
                principalTable: "AppJob",
                principalColumn: "JobID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEmployee_AppJob_JobID",
                table: "AppEmployee");

            migrationBuilder.DropIndex(
                name: "IX_AppEmployee_JobID",
                table: "AppEmployee");
        }
    }
}
