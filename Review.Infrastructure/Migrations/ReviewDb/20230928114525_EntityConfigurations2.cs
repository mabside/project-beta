using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.Infrastructure.Migrations.ReviewDb
{
    /// <inheritdoc />
    public partial class EntityConfigurations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Link",
                newName: "Link",
                newSchema: "Business");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Link",
                schema: "Business",
                newName: "Link");
        }
    }
}
