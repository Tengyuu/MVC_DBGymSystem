using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_DBGym.Migrations
{
    /// <inheritdoc />
    public partial class InitialFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coache",
                columns: table => new
                {
                    CoachID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Speciality = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coache", x => x.CoachID);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "PTyp",
                columns: table => new
                {
                    PTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PTyp", x => x.PTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    CourseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoachID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Course_Coache_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coache",
                        principalColumn: "CoachID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payment_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserve",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserve", x => new { x.MemberID, x.CourseID });
                    table.ForeignKey(
                        name: "FK_Reserve_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserve_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    PTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => new { x.PaymentID, x.PTypeID });
                    table.ForeignKey(
                        name: "FK_PaymentType_PTyp_PTypeID",
                        column: x => x.PTypeID,
                        principalTable: "PTyp",
                        principalColumn: "PTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentType_Payment_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payment",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CoachID",
                table: "Course",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_MemberID",
                table: "Payment",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentType_PTypeID",
                table: "PaymentType",
                column: "PTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserve_CourseID",
                table: "Reserve",
                column: "CourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "Reserve");

            migrationBuilder.DropTable(
                name: "PTyp");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Coache");
        }
    }
}
