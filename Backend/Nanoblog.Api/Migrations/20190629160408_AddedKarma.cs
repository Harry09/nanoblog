using Microsoft.EntityFrameworkCore.Migrations;

namespace Nanoblog.Api.Migrations
{
    public partial class AddedKarma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentKarma",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    CommentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentKarma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentKarma_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentKarma_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryKarma",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    EntryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryKarma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryKarma_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntryKarma_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentKarma_AuthorId",
                table: "CommentKarma",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentKarma_CommentId",
                table: "CommentKarma",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryKarma_AuthorId",
                table: "EntryKarma",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryKarma_EntryId",
                table: "EntryKarma",
                column: "EntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentKarma");

            migrationBuilder.DropTable(
                name: "EntryKarma");
        }
    }
}
