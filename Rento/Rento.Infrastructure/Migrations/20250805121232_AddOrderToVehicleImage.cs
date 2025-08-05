using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderToVehicleImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "VehicleImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "VehicleImages");
        }
    }
}
