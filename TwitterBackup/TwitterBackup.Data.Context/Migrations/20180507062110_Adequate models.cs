using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackup.Data.Context.Migrations
{
    public partial class Adequatemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTweet_Tweets_TweetId",
                table: "UserTweet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTweet_AspNetUsers_UserId",
                table: "UserTweet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTweet",
                table: "UserTweet");

            migrationBuilder.RenameTable(
                name: "UserTweet",
                newName: "UserTweets");

            migrationBuilder.RenameIndex(
                name: "IX_UserTweet_TweetId",
                table: "UserTweets",
                newName: "IX_UserTweets_TweetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTweets",
                table: "UserTweets",
                columns: new[] { "UserId", "TweetId" });

            migrationBuilder.CreateTable(
                name: "UserTweeters",
                columns: table => new
                {
                    TweeterId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTweeters", x => new { x.TweeterId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTweeters_Tweeters_TweeterId",
                        column: x => x.TweeterId,
                        principalTable: "Tweeters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTweeters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTweeters_UserId",
                table: "UserTweeters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTweets_Tweets_TweetId",
                table: "UserTweets",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTweets_AspNetUsers_UserId",
                table: "UserTweets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTweets_Tweets_TweetId",
                table: "UserTweets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTweets_AspNetUsers_UserId",
                table: "UserTweets");

            migrationBuilder.DropTable(
                name: "UserTweeters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTweets",
                table: "UserTweets");

            migrationBuilder.RenameTable(
                name: "UserTweets",
                newName: "UserTweet");

            migrationBuilder.RenameIndex(
                name: "IX_UserTweets_TweetId",
                table: "UserTweet",
                newName: "IX_UserTweet_TweetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTweet",
                table: "UserTweet",
                columns: new[] { "UserId", "TweetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserTweet_Tweets_TweetId",
                table: "UserTweet",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTweet_AspNetUsers_UserId",
                table: "UserTweet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
