using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GGCasualNote.Migrations
{
    public partial class UpdateComboNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LongestStreak",
                table: "ComboNotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongestStreak",
                table: "ComboNotes");
        }
    }
}
