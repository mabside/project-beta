using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.Infrastructure.Migrations.ReviewDb
{
    /// <inheritdoc />
    public partial class EntityModification4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Link_LinkId",
                schema: "Business",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_LinkId",
                schema: "Business",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "LinkId",
                schema: "Business",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "Business",
                table: "Item",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "Business",
                table: "Business",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "Business",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "Business",
                table: "Business");

            migrationBuilder.AddColumn<Guid>(
                name: "LinkId",
                schema: "Business",
                table: "Item",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Item_LinkId",
                schema: "Business",
                table: "Item",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Link_LinkId",
                schema: "Business",
                table: "Item",
                column: "LinkId",
                principalSchema: "Business",
                principalTable: "Link",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
