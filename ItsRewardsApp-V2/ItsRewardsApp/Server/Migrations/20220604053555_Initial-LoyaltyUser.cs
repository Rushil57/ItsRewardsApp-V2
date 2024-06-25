using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItsRewardsApp.Server.Migrations
{
    public partial class InitialLoyaltyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoyaltyUserProfile",
                columns: table => new
                {
                    UserProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevenueCenterID = table.Column<int>(type: "int", nullable: true),
                    isDelete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true),
                    CustomerGroup = table.Column<int>(type: "int", nullable: true),
                    CustomerStatus = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceLevel = table.Column<int>(type: "int", nullable: true),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeVerified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppVerified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPass = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pinnumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyUserProfile", x => x.UserProfileID);
                });

            migrationBuilder.CreateTable(
                name: "UserLoyaltyStoreMappings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastPurchase = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoyaltyStoreMappings", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoyaltyUserProfile");

            migrationBuilder.DropTable(
                name: "UserLoyaltyStoreMappings");
        }
    }
}
