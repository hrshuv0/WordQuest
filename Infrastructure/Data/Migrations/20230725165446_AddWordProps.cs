using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWordProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswer",
                table: "Word",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Word",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Definition",
                table: "Word",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevel",
                table: "Word",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Example",
                table: "Word",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Word",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "PartOfSpeech",
                table: "Word",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pronunciation",
                table: "Word",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Translation",
                table: "Word",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WrongAnswer",
                table: "Word",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "Definition",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "Example",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "PartOfSpeech",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "Pronunciation",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "Translation",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "WrongAnswer",
                table: "Word");
        }
    }
}
