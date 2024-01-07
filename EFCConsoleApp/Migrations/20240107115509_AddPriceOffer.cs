using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PromotionalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    PromotionalText = table.Column<string>(type: "TEXT", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceOffer_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceOffer_BookId",
                table: "PriceOffer",
                column: "BookId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceOffer");
        }
    }
}
