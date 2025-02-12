using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderSystem_J103042.Migrations
{
    
    public partial class AddImageUrlToFoodItem : Migration
    {
       
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItems");
        }
    }
}
