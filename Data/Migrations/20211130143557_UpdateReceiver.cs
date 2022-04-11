using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeroHunger.Migrations
{
    public partial class UpdateReceiver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receiver_SalaryGroup_ReceiverSalaryGroup",
                table: "Receiver");

            migrationBuilder.DropForeignKey(
                name: "FK_Receiver_User_User",
                table: "Receiver");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiverFamily_Receiver_Receiver",
                table: "ReceiverFamily");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiverFamily_SalaryGroup_FamilySalaryGroup",
                table: "ReceiverFamily");

            migrationBuilder.DropIndex(
                name: "IX_ReceiverFamily_FamilySalaryGroup",
                table: "ReceiverFamily");

            migrationBuilder.DropIndex(
                name: "IX_ReceiverFamily_Receiver",
                table: "ReceiverFamily");

            migrationBuilder.DropIndex(
                name: "IX_Receiver_ReceiverSalaryGroup",
                table: "Receiver");

            migrationBuilder.DropIndex(
                name: "IX_Receiver_User",
                table: "Receiver");

            migrationBuilder.DropColumn(
                name: "FamilySalaryGroup",
                table: "ReceiverFamily");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "ReceiverFamily");

            migrationBuilder.DropColumn(
                name: "ReceiverSalaryGroup",
                table: "Receiver");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Receiver");

            migrationBuilder.AlterColumn<string>(
                name: "receiverIC",
                table: "ReceiverFamily",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiverFamily_familySalaryGroupID",
                table: "ReceiverFamily",
                column: "familySalaryGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiverFamily_receiverIC",
                table: "ReceiverFamily",
                column: "receiverIC");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_receiverSalaryGroupID",
                table: "Receiver",
                column: "receiverSalaryGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_userID",
                table: "Receiver",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receiver_SalaryGroup_receiverSalaryGroupID",
                table: "Receiver",
                column: "receiverSalaryGroupID",
                principalTable: "SalaryGroup",
                principalColumn: "salaryGroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receiver_User_userID",
                table: "Receiver",
                column: "userID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiverFamily_Receiver_receiverIC",
                table: "ReceiverFamily",
                column: "receiverIC",
                principalTable: "Receiver",
                principalColumn: "receiverIC");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiverFamily_SalaryGroup_familySalaryGroupID",
                table: "ReceiverFamily",
                column: "familySalaryGroupID",
                principalTable: "SalaryGroup",
                principalColumn: "salaryGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receiver_SalaryGroup_receiverSalaryGroupID",
                table: "Receiver");

            migrationBuilder.DropForeignKey(
                name: "FK_Receiver_User_userID",
                table: "Receiver");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiverFamily_Receiver_receiverIC",
                table: "ReceiverFamily");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiverFamily_SalaryGroup_familySalaryGroupID",
                table: "ReceiverFamily");

            migrationBuilder.DropIndex(
                name: "IX_ReceiverFamily_familySalaryGroupID",
                table: "ReceiverFamily");

            migrationBuilder.DropIndex(
                name: "IX_ReceiverFamily_receiverIC",
                table: "ReceiverFamily");

            migrationBuilder.DropIndex(
                name: "IX_Receiver_receiverSalaryGroupID",
                table: "Receiver");

            migrationBuilder.DropIndex(
                name: "IX_Receiver_userID",
                table: "Receiver");

            migrationBuilder.AlterColumn<string>(
                name: "receiverIC",
                table: "ReceiverFamily",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)");

            migrationBuilder.AddColumn<int>(
                name: "FamilySalaryGroup",
                table: "ReceiverFamily",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "ReceiverFamily",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverSalaryGroup",
                table: "Receiver",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User",
                table: "Receiver",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiverFamily_FamilySalaryGroup",
                table: "ReceiverFamily",
                column: "FamilySalaryGroup");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiverFamily_Receiver",
                table: "ReceiverFamily",
                column: "Receiver");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_ReceiverSalaryGroup",
                table: "Receiver",
                column: "ReceiverSalaryGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_User",
                table: "Receiver",
                column: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Receiver_SalaryGroup_ReceiverSalaryGroup",
                table: "Receiver",
                column: "ReceiverSalaryGroup",
                principalTable: "SalaryGroup",
                principalColumn: "salaryGroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receiver_User_User",
                table: "Receiver",
                column: "User",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiverFamily_Receiver_Receiver",
                table: "ReceiverFamily",
                column: "Receiver",
                principalTable: "Receiver",
                principalColumn: "receiverIC");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiverFamily_SalaryGroup_FamilySalaryGroup",
                table: "ReceiverFamily",
                column: "FamilySalaryGroup",
                principalTable: "SalaryGroup",
                principalColumn: "salaryGroupID");
        }
    }
}
