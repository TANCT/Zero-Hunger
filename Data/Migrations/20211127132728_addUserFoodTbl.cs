using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero_Hunger.Migrations
{
    public partial class addUserFoodTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TypeInterface = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserAdrs1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAdrs2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_UserType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "UserType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookedFoodDonation",
                columns: table => new
                {
                    CookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookQuantity = table.Column<int>(type: "int", nullable: false),
                    CookLongtitude = table.Column<float>(type: "real", nullable: false),
                    CookLatitude = table.Column<float>(type: "real", nullable: false),
                    DonorUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookedFoodDonation", x => x.CookID);
                    table.ForeignKey(
                        name: "FK_CookedFoodDonation_User_DonorUserID",
                        column: x => x.DonorUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    DonorId = table.Column<int>(type: "int", nullable: true),
                    DelivererId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DryFoodDonation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DryFoodDonation_User_DelivererId",
                        column: x => x.DelivererId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DryFoodDonation_User_DonorId",
                        column: x => x.DonorId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CookedFoodDonation_DonorUserID",
                table: "CookedFoodDonation",
                column: "DonorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DryFoodDonation_DelivererId",
                table: "DryFoodDonation",
                column: "DelivererId");

            migrationBuilder.CreateIndex(
                name: "IX_DryFoodDonation_DonorId",
                table: "DryFoodDonation",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TypeId",
                table: "User",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookedFoodDonation");

            migrationBuilder.DropTable(
                name: "DryFoodDonation");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
