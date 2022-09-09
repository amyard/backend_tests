using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthMVC.Migrations
{
    public partial class AddFriends2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendShips",
                table: "FriendShips");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryProfileId",
                table: "FriendShips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendShips",
                table: "FriendShips",
                columns: new[] { "PrimaryProfileId", "FriendProfileId" });

            migrationBuilder.CreateIndex(
                name: "IX_FriendShips_FriendProfileId",
                table: "FriendShips",
                column: "FriendProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShips_Profiles_PrimaryProfileId",
                table: "FriendShips",
                column: "PrimaryProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendShips_Profiles_PrimaryProfileId",
                table: "FriendShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendShips",
                table: "FriendShips");

            migrationBuilder.DropIndex(
                name: "IX_FriendShips_FriendProfileId",
                table: "FriendShips");

            migrationBuilder.DropColumn(
                name: "PrimaryProfileId",
                table: "FriendShips");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendShips",
                table: "FriendShips",
                column: "FriendProfileId");
        }
    }
}
