using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionService.Migrations
{
    /// <inheritdoc />
    public partial class changeToItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FieldText",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FieldString",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FieldInt",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FieldDate",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FieldBool",
                newName: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "FieldText",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "FieldString",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "FieldInt",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "FieldDate",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "FieldBool",
                newName: "ApplicationUserId");
        }
    }
}
