using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PelatihanHari2Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationTableCourseAndEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CourseID",
                schema: "fauzansaef",
                table: "Enrollment",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentID",
                schema: "fauzansaef",
                table: "Enrollment",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Course_CourseID",
                schema: "fauzansaef",
                table: "Enrollment",
                column: "CourseID",
                principalSchema: "fauzansaef",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Students_StudentID",
                schema: "fauzansaef",
                table: "Enrollment",
                column: "StudentID",
                principalSchema: "fauzansaef",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Course_CourseID",
                schema: "fauzansaef",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Students_StudentID",
                schema: "fauzansaef",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_CourseID",
                schema: "fauzansaef",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_StudentID",
                schema: "fauzansaef",
                table: "Enrollment");
        }
    }
}
