using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogn.Data.Migrations
{
    public partial class AddResetTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResetTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateExpired = table.Column<DateTimeOffset>(nullable: false),
                    DateConsumed = table.Column<DateTimeOffset>(nullable: true),
                    CredentialsAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetToken", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_ResetTokens_Credentials_CredentialsAccountId",
                        column: x => x.CredentialsAccountId,
                        principalTable: "Credentials",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResetTokens_CredentialsAccountId",
                table: "ResetTokens",
                column: "CredentialsAccountId");

            migrationBuilder.CreateIndex(
                name: "UK_ResetToken_Token",
                table: "ResetTokens",
                column: "Token",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResetTokens");
        }
    }
}
