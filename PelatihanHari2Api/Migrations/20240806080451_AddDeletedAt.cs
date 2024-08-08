using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PelatihanHari2Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "fauzansaef",
                table: "Enrollment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "fauzansaef",
                table: "Course",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "fauzansaef",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "fauzansaef",
                table: "Course");
        }
    }
}
