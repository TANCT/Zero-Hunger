using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeroHunger.Migrations
{
    public partial class AddReceiverToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaryGroup",
                columns: table => new
                {
                    salaryGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salaryRange = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryGroup", x => x.salaryGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Receiver",
                columns: table => new
                {
                    receiverIC = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    receiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    receiverGender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    receiverDOB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    receiverOccupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverSalaryGroup = table.Column<int>(type: "int", nullable: false),
                    receiverSalaryGroupID = table.Column<int>(type: "int", nullable: false),
                    receiverFamilyNo = table.Column<int>(type: "int", nullable: false),
                    receiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    receiverPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    receiverAdrs1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    receiverAdrs2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    applicationStatusID = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<int>(type: "int", nullable: true),
                    userID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receiver", x => x.receiverIC);
                    table.ForeignKey(
                        name: "FK_Receiver_SalaryGroup_ReceiverSalaryGroup",
                        column: x => x.ReceiverSalaryGroup,
                        principalTable: "SalaryGroup",
                        principalColumn: "salaryGroupID");
                    table.ForeignKey(
                        name: "FK_Receiver_User_User",
                        column: x => x.User,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ReceiverFamily",
                columns: table => new
                {
                    familyIC = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    receiverIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    familyDOB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    familyOccupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilySalaryGroup = table.Column<int>(type: "int", nullable: false),
                    familySalaryGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiverFamily", x => x.familyIC);
                    table.ForeignKey(
                        name: "FK_ReceiverFamily_Receiver_Receiver",
                        column: x => x.Receiver,
                        principalTable: "Receiver",
                        principalColumn: "receiverIC");
                    table.ForeignKey(
                        name: "FK_ReceiverFamily_SalaryGroup_FamilySalaryGroup",
                        column: x => x.FamilySalaryGroup,
                        principalTable: "SalaryGroup",
                        principalColumn: "salaryGroupID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_ReceiverSalaryGroup",
                table: "Receiver",
                column: "ReceiverSalaryGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_User",
                table: "Receiver",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiverFamily_FamilySalaryGroup",
                table: "ReceiverFamily",
                column: "FamilySalaryGroup");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiverFamily_Receiver",
                table: "ReceiverFamily",
                column: "Receiver");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiverFamily");

            migrationBuilder.DropTable(
                name: "Receiver");

            migrationBuilder.DropTable(
                name: "SalaryGroup");
        }
    }
}
