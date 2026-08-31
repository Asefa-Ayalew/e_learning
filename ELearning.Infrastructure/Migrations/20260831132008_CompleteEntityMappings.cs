using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELearning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompleteEntityMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_sections_course_id",
                table: "sections");

            migrationBuilder.DropIndex(
                name: "ix_lessons_section_id",
                table: "lessons");

            migrationBuilder.DropIndex(
                name: "ix_lesson_progresses_user_id",
                table: "lesson_progresses");

            migrationBuilder.DropIndex(
                name: "ix_course_reviews_course_id",
                table: "course_reviews");

            migrationBuilder.AlterColumn<bool>(
                name: "is_email_verified",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "sections",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "video_url",
                table: "lessons",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "lessons",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "certificate_url",
                table: "certificates",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "certificate_number",
                table: "certificates",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_sections_course_id_display_order",
                table: "sections",
                columns: new[] { "course_id", "display_order" });

            migrationBuilder.CreateIndex(
                name: "ix_lessons_section_id_display_order",
                table: "lessons",
                columns: new[] { "section_id", "display_order" });

            migrationBuilder.CreateIndex(
                name: "ix_lesson_progresses_user_id_lesson_id",
                table: "lesson_progresses",
                columns: new[] { "user_id", "lesson_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_course_reviews_course_id_user_id",
                table: "course_reviews",
                columns: new[] { "course_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_certificates_certificate_number",
                table: "certificates",
                column: "certificate_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_sections_course_id_display_order",
                table: "sections");

            migrationBuilder.DropIndex(
                name: "ix_lessons_section_id_display_order",
                table: "lessons");

            migrationBuilder.DropIndex(
                name: "ix_lesson_progresses_user_id_lesson_id",
                table: "lesson_progresses");

            migrationBuilder.DropIndex(
                name: "ix_course_reviews_course_id_user_id",
                table: "course_reviews");

            migrationBuilder.DropIndex(
                name: "ix_certificates_certificate_number",
                table: "certificates");

            migrationBuilder.AlterColumn<bool>(
                name: "is_email_verified",
                table: "users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "sections",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "video_url",
                table: "lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "lessons",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "certificate_url",
                table: "certificates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "certificate_number",
                table: "certificates",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "ix_sections_course_id",
                table: "sections",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_lessons_section_id",
                table: "lessons",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_lesson_progresses_user_id",
                table: "lesson_progresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_reviews_course_id",
                table: "course_reviews",
                column: "course_id");
        }
    }
}
