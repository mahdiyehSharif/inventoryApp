using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddProductLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLimit_AppEmployee_EmployeeID",
                table: "ProductLimit");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLimit_AppJob_JobID",
                table: "ProductLimit");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLimit_Products_ProductID",
                table: "ProductLimit");

            migrationBuilder.DropIndex(
                name: "IX_ProductLimit_EmployeeID",
                table: "ProductLimit");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "ProductLimit",
                newName: "PeriodValue");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductID",
                table: "ProductLimit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobID",
                table: "ProductLimit",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductLimit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxQuantity",
                table: "ProductLimit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeriodType",
                table: "ProductLimit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLimit_AppJob_JobID",
                table: "ProductLimit",
                column: "JobID",
                principalTable: "AppJob",
                principalColumn: "JobID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLimit_Products_ProductID",
                table: "ProductLimit",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLimit_AppJob_JobID",
                table: "ProductLimit");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLimit_Products_ProductID",
                table: "ProductLimit");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductLimit");

            migrationBuilder.DropColumn(
                name: "MaxQuantity",
                table: "ProductLimit");

            migrationBuilder.DropColumn(
                name: "PeriodType",
                table: "ProductLimit");

            migrationBuilder.RenameColumn(
                name: "PeriodValue",
                table: "ProductLimit",
                newName: "EmployeeID");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductID",
                table: "ProductLimit",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "JobID",
                table: "ProductLimit",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLimit_EmployeeID",
                table: "ProductLimit",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLimit_AppEmployee_EmployeeID",
                table: "ProductLimit",
                column: "EmployeeID",
                principalTable: "AppEmployee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLimit_AppJob_JobID",
                table: "ProductLimit",
                column: "JobID",
                principalTable: "AppJob",
                principalColumn: "JobID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLimit_Products_ProductID",
                table: "ProductLimit",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID");
        }
    }
}
