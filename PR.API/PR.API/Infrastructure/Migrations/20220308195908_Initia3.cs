using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR.API.Infrastructure.Migrations
{
    public partial class Initia3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_FriendRequestStatus_FriendRequestStatusId1",
                schema: "pr.service",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_FriendRequestStatusId1",
                schema: "pr.service",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "FriendRequestStatusId1",
                schema: "pr.service",
                table: "FriendRequests");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_FriendRequestStatusId",
                schema: "pr.service",
                table: "FriendRequests",
                column: "FriendRequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_FriendRequestStatus_FriendRequestStatusId",
                schema: "pr.service",
                table: "FriendRequests",
                column: "FriendRequestStatusId",
                principalSchema: "pr.service",
                principalTable: "FriendRequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_FriendRequestStatus_FriendRequestStatusId",
                schema: "pr.service",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_FriendRequestStatusId",
                schema: "pr.service",
                table: "FriendRequests");

            migrationBuilder.AddColumn<int>(
                name: "FriendRequestStatusId1",
                schema: "pr.service",
                table: "FriendRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_FriendRequestStatusId1",
                schema: "pr.service",
                table: "FriendRequests",
                column: "FriendRequestStatusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_FriendRequestStatus_FriendRequestStatusId1",
                schema: "pr.service",
                table: "FriendRequests",
                column: "FriendRequestStatusId1",
                principalSchema: "pr.service",
                principalTable: "FriendRequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
