using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR.API.Infrastructure.Migrations
{
    public partial class Initia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pr.service");

            migrationBuilder.CreateSequence(
                name: "friendrequestseq",
                schema: "pr.service",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "friendshipseq",
                schema: "pr.service",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "personseq",
                schema: "pr.service",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "FriendRequestStatus",
                schema: "pr.service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequestStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "friendships",
                schema: "pr.service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SenderIdentityGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverIdentityGuid = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                schema: "pr.service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdentityGuid = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                schema: "pr.service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SenderIdentityGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverIdentityGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FriendRequestStatusId1 = table.Column<int>(type: "int", nullable: false),
                    FriendshipId = table.Column<int>(type: "int", nullable: true),
                    FriendRequestStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequests_FriendRequestStatus_FriendRequestStatusId1",
                        column: x => x.FriendRequestStatusId1,
                        principalSchema: "pr.service",
                        principalTable: "FriendRequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendRequests_friendships_FriendshipId",
                        column: x => x.FriendshipId,
                        principalSchema: "pr.service",
                        principalTable: "friendships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_FriendRequestStatusId1",
                schema: "pr.service",
                table: "FriendRequests",
                column: "FriendRequestStatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_FriendshipId",
                schema: "pr.service",
                table: "FriendRequests",
                column: "FriendshipId");

            migrationBuilder.CreateIndex(
                name: "IX_persons_IdentityGuid",
                schema: "pr.service",
                table: "persons",
                column: "IdentityGuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendRequests",
                schema: "pr.service");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "pr.service");

            migrationBuilder.DropTable(
                name: "FriendRequestStatus",
                schema: "pr.service");

            migrationBuilder.DropTable(
                name: "friendships",
                schema: "pr.service");

            migrationBuilder.DropSequence(
                name: "friendrequestseq",
                schema: "pr.service");

            migrationBuilder.DropSequence(
                name: "friendshipseq",
                schema: "pr.service");

            migrationBuilder.DropSequence(
                name: "personseq",
                schema: "pr.service");
        }
    }
}
