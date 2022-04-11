using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero_Hunger.Migrations
{
    public partial class updateCookFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                table: "CookedFoodDonation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OpenDate",
                table: "CookedFoodDonation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RemainQuantity",
                table: "CookedFoodDonation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Reservation",
                table: "CookedFoodDonation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShopName",
                table: "CookedFoodDonation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "CookedFoodDonation");

            migrationBuilder.DropColumn(
                name: "OpenDate",
                table: "CookedFoodDonation");

            migrationBuilder.DropColumn(
                name: "RemainQuantity",
                table: "CookedFoodDonation");

            migrationBuilder.DropColumn(
                name: "Reservation",
                table: "CookedFoodDonation");

            migrationBuilder.DropColumn(
                name: "ShopName",
                table: "CookedFoodDonation");
        }
    }
}
