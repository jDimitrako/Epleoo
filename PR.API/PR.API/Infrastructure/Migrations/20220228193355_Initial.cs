using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PR.API.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pr.service");

            migrationBuilder.CreateSequence(
                name: "personseq",
                schema: "pr.service",
                incrementBy: 10);

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
                name: "FriendRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequest_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "pr.service",
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendship_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "pr.service",
                        principalTable: "persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_PersonId",
                table: "FriendRequest",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_PersonId",
                table: "Friendship",
                column: "PersonId");

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
                name: "FriendRequest");

            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "pr.service");

            migrationBuilder.DropSequence(
                name: "personseq",
                schema: "pr.service");
        }
    }
}
