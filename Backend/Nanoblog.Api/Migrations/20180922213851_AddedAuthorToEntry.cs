using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Nanoblog.Api.Migrations
{
    public partial class AddedAuthorToEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Entries",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_AuthorId",
                table: "Entries",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Users_AuthorId",
                table: "Entries",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Users_AuthorId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_AuthorId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Entries");
        }
    }
}
