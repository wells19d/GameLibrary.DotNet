using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.DotNet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "Games",
                newName: "Summary");

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EarlyAccess",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Franchise",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Games",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Developer",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "EarlyAccess",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Franchise",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Games",
                newName: "Subtitle");
        }
    }
}
