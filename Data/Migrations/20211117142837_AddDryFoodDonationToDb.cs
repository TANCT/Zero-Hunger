using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeroHunger.Data.Migrations
{
    public partial class AddDryFoodDonationToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DryFoodDonation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DryFoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DryFoodQuantity = table.Column<int>(type: "int", nullable: false),
                    DryFoodPickDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DryFoodRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelivererId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryFoodDonation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DryFoodDonation");
        }
    }
}
