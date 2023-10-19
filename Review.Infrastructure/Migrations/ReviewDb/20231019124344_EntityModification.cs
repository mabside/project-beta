using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.Infrastructure.Migrations.ReviewDb
{
    /// <inheritdoc />
    public partial class EntityModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FeedbackType",
                schema: "Feedback",
                table: "Feedback",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FeedbackType",
                schema: "Feedback",
                table: "Feedback",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
