using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedReservationConnectionOnReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reservations_ReservationId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReservationId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReservationId1",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId1",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReservationId1",
                table: "Reviews",
                column: "ReservationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reservations_ReservationId1",
                table: "Reviews",
                column: "ReservationId1",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
