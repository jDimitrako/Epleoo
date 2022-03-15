using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR.API.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "friendrequestseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "personseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "friendRequestStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendRequestStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
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
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SenderPersonId = table.Column<int>(type: "int", nullable: false),
                    ReceiverPersonId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Modifier = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FriendRequestStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequests_friendRequestStatus_FriendRequestStatusId",
                        column: x => x.FriendRequestStatusId,
                        principalTable: "friendRequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "friendships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_friendships_persons_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_friendships_persons_SenderId",
                        column: x => x.SenderId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_FriendRequestStatusId",
                table: "FriendRequests",
                column: "FriendRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_friendships_ReceiverId",
                table: "friendships",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_friendships_SenderId",
                table: "friendships",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_persons_IdentityGuid",
                table: "persons",
                column: "IdentityGuid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "friendships");

            migrationBuilder.DropTable(
                name: "friendRequestStatus");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropSequence(
                name: "friendrequestseq");

            migrationBuilder.DropSequence(
                name: "personseq");
        }
    }
}
