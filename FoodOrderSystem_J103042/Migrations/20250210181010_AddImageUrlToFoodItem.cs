using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderSystem_J103042.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToFoodItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Item_Desc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: true),
                    Vegetarian = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItems");
        }
    }
}
