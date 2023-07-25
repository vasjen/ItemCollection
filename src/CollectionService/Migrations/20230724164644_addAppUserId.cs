using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionService.Migrations
{
    /// <inheritdoc />
    public partial class addAppUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "FieldText",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "FieldString",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "FieldInt",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "FieldDate",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "FieldBool",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FieldText");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FieldString");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FieldInt");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FieldDate");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FieldBool");
        }
    }
}
