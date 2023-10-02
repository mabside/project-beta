using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.Infrastructure.Migrations.ReviewDb
{
    /// <inheritdoc />
    public partial class EntityConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Space");

            migrationBuilder.RenameTable(
                name: "Space",
                newName: "Space",
                newSchema: "Space");

            migrationBuilder.RenameTable(
                name: "ItemCategory",
                newName: "ItemCategory",
                newSchema: "Business");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Item",
                newSchema: "Business");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Space",
                table: "Space",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Space",
                table: "Space",
                type: "character varying(225)",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Business",
                table: "ItemCategory",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Business",
                table: "ItemCategory",
                type: "character varying(225)",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Business",
                table: "Item",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Business",
                table: "Item",
                type: "character varying(225)",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Space",
                schema: "Space",
                newName: "Space");

            migrationBuilder.RenameTable(
                name: "ItemCategory",
                schema: "Business",
                newName: "ItemCategory");

            migrationBuilder.RenameTable(
                name: "Item",
                schema: "Business",
                newName: "Item");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Space",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Space",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(225)",
                oldMaxLength: 225);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemCategory",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ItemCategory",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(225)",
                oldMaxLength: 225);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Item",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Item",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(225)",
                oldMaxLength: 225);
        }
    }
}
