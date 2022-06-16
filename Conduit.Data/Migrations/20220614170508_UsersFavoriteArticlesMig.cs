using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conduit.Data.Migrations
{
    public partial class UsersFavoriteArticlesMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersFavoriteArticles",
                columns: table => new
                {
                    FavoritedArticlesId = table.Column<int>(type: "int", nullable: false),
                    FavoritesUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFavoriteArticles", x => new { x.FavoritedArticlesId, x.FavoritesUsersId });
                    table.ForeignKey(
                        name: "FK_UsersFavoriteArticles_Articles_FavoritedArticlesId",
                        column: x => x.FavoritedArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersFavoriteArticles_Users_FavoritesUsersId",
                        column: x => x.FavoritesUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersFavoriteArticles_FavoritesUsersId",
                table: "UsersFavoriteArticles",
                column: "FavoritesUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersFavoriteArticles");
        }
    }
}
