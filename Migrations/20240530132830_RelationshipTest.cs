using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GGCasualNote.Migrations
{
    public partial class RelationshipTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ComboNotes_CharacterId",
                table: "ComboNotes",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboNotes_Characters_CharacterId",
                table: "ComboNotes",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboNotes_Characters_CharacterId",
                table: "ComboNotes");

            migrationBuilder.DropIndex(
                name: "IX_ComboNotes_CharacterId",
                table: "ComboNotes");
        }
    }
}
