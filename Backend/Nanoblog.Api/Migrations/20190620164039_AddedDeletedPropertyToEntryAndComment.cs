using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanoblog.Api.Migrations
{
    public partial class AddedDeletedPropertyToEntryAndComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Entries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Comments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Comments");
        }
    }
}
