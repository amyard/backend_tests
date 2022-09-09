using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthMVC.Migrations
{
    public partial class AddFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendShips",
                columns: table => new
                {
                    FriendProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendShips", x => x.FriendProfileId);
                    table.ForeignKey(
                        name: "FK_FriendShips_Profiles_FriendProfileId",
                        column: x => x.FriendProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendShips");
        }
    }
}
