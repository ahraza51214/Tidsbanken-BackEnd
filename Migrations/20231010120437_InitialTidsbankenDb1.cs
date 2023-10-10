using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tidsbanken_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialTidsbankenDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IneligiblePeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IneligiblePeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IneligiblePeriods_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationRequests_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VacationRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateCommented = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusAtTimeOfComment = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_VacationRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "VacationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Employee" },
                    { 2, "Admin" },
                    { 3, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", "hashed_password1", 1, "employee1" },
                    { 2, "jane.smith@example.com", "Jane", "Smith", "hashed_password2", 1, "employee2" },
                    { 3, "admin@example.com", "Admin", "Admin", "hashed_password3", 2, "admin1" },
                    { 4, "manager@example.com", "Manager", "Manager", "hashed_password4", 3, "manager1" },
                    { 5, "sarah.johnson@example.com", "Sarah", "Johnson", "hashed_password5", 1, "employee3" }
                });

            migrationBuilder.InsertData(
                table: "IneligiblePeriods",
                columns: new[] { "Id", "Description", "EndDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Vacation blackout period 1", new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), 3 },
                    { 2, "Vacation blackout period 2", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), 4 },
                    { 3, "Vacation blackout period 3", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 4 },
                    { 4, "Vacation blackout period 4", new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Local), 3 },
                    { 5, "Vacation blackout period 5", new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), 2 }
                });

            migrationBuilder.InsertData(
                table: "VacationRequests",
                columns: new[] { "Id", "ApprovalDate", "ApproverId", "EndDate", "RequestDate", "StartDate", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 10, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6600), new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), "Pending", 1 },
                    { 2, null, null, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 11, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6610), new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), "Approved", 2 },
                    { 3, null, null, new DateTime(2023, 12, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 12, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6620), new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Local), "Pending", 3 },
                    { 4, null, null, new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 11, 25, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6620), new DateTime(2023, 11, 25, 0, 0, 0, 0, DateTimeKind.Local), "Approved", 4 },
                    { 5, null, null, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6630), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Local), "Pending", 5 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "DateCommented", "Message", "RequestId", "StatusAtTimeOfComment", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6710), "This is a comment by John.", 1, 0, null },
                    { 2, new DateTime(2023, 10, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6710), "This is a comment by Manager.", 2, 2, null },
                    { 3, new DateTime(2023, 10, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6720), "Another comment by Manager.", 3, 0, null },
                    { 4, new DateTime(2023, 10, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6720), "A comment by Admin.", 4, 2, null },
                    { 5, new DateTime(2023, 10, 10, 14, 4, 37, 134, DateTimeKind.Local).AddTicks(6720), "A comment by Jane.", 5, 0, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RequestId",
                table: "Comments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IneligiblePeriods_UserId",
                table: "IneligiblePeriods",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_ApproverId",
                table: "VacationRequests",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_UserId",
                table: "VacationRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "IneligiblePeriods");

            migrationBuilder.DropTable(
                name: "VacationRequests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
