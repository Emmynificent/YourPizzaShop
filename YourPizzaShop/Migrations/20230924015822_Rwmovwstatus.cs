using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourPizzaShop.Migrations
{
    /// <inheritdoc />
    public partial class Rwmovwstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PizzaOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PizzaOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
