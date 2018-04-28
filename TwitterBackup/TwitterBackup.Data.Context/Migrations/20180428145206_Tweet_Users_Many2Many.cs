using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Context.Migrations
{
    public partial class Tweet_Users_Many2Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_AspNetUsers_UserId",
                table: "Tweets");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_UserId",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tweets");

            migrationBuilder.CreateTable(
                name: "UserTweet",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TweetId = table.Column<int>(nullable: false),
                    TweetId1 = table.Column<long>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTweet", x => new { x.UserId, x.TweetId });
                    table.ForeignKey(
                        name: "FK_UserTweet_Tweets_TweetId1",
                        column: x => x.TweetId1,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTweet_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTweet_TweetId1",
                table: "UserTweet",
                column: "TweetId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserTweet_UserId1",
                table: "UserTweet",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTweet");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tweets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_UserId",
                table: "Tweets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_AspNetUsers_UserId",
                table: "Tweets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
