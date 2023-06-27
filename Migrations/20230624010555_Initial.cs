using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondhandStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    roleId = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    roleName = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    accountId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    password = table.Column<string>(type: "char(64)", unicode: false, fixedLength: true, maxLength: 64, nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    roleId = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phoneNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    userRatingScore = table.Column<double>(type: "float", nullable: false),
                    pointBalance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.accountId);
                    table.ForeignKey(
                        name: "FK_Account_Role_roleId",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    permissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permissionName = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    roleId = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.permissionId);
                    table.ForeignKey(
                        name: "FK__Permissio__roleI__2E1BDC42",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "roleId");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    postId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    productName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    postStatus = table.Column<bool>(type: "bit", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    postType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    pointCost = table.Column<int>(type: "int", nullable: false),
                    postDate = table.Column<DateTime>(type: "date", nullable: false),
                    postPriority = table.Column<int>(type: "int", nullable: false),
                    postExpiryDate = table.Column<DateTime>(type: "date", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.postId);
                    table.ForeignKey(
                        name: "FK__Post__accountId__286302EC",
                        column: x => x.accountId,
                        principalTable: "Account",
                        principalColumn: "accountId");
                    table.ForeignKey(
                        name: "FK__Post__categoryId__29572725",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "categoryId");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    reportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reason = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    reportedAccountId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    reportDate = table.Column<DateTime>(type: "date", nullable: false),
                    evidence1 = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    evidence2 = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    evidence3 = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.reportId);
                    table.ForeignKey(
                        name: "FK__Report__reported__403A8C7D",
                        column: x => x.reportedAccountId,
                        principalTable: "Account",
                        principalColumn: "accountId");
                });

            migrationBuilder.CreateTable(
                name: "TopUp",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    topUpPoint = table.Column<int>(type: "int", nullable: false),
                    accountId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    topUpDate = table.Column<DateTime>(type: "date", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TopUp__0809335D46A767E4", x => x.orderId);
                    table.ForeignKey(
                        name: "FK__TopUp__accountId__30F848ED",
                        column: x => x.accountId,
                        principalTable: "Account",
                        principalColumn: "accountId");
                });

            migrationBuilder.CreateTable(
                name: "ExchangeOrder",
                columns: table => new
                {
                    orderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postId = table.Column<int>(type: "int", nullable: false),
                    accountID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    receiverId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    orderDate = table.Column<DateTime>(type: "date", nullable: false),
                    orderStatus = table.Column<bool>(type: "bit", nullable: false),
                    receiverPhoneNumber = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    receiverEmail = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exchange__E4FEDE4A0591CCC7", x => x.orderDetailId);
                    table.ForeignKey(
                        name: "FK__ExchangeO__accou__34C8D9D1",
                        column: x => x.accountID,
                        principalTable: "Account",
                        principalColumn: "accountId");
                    table.ForeignKey(
                        name: "FK__ExchangeO__postI__35BCFE0A",
                        column: x => x.postId,
                        principalTable: "Post",
                        principalColumn: "postId");
                    table.ForeignKey(
                        name: "FK__ExchangeO__recei__33D4B598",
                        column: x => x.receiverId,
                        principalTable: "Account",
                        principalColumn: "accountId");
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRequest",
                columns: table => new
                {
                    requestDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sellerId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    orderDate = table.Column<DateTime>(type: "date", nullable: false),
                    postId = table.Column<int>(type: "int", nullable: false),
                    sellerPhoneNumber = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    sellerEmail = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exchange__6FB55063FA6005F9", x => x.requestDetailId);
                    table.ForeignKey(
                        name: "FK__ExchangeR__postI__398D8EEE",
                        column: x => x.postId,
                        principalTable: "Post",
                        principalColumn: "postId");
                    table.ForeignKey(
                        name: "FK__ExchangeR__selle__38996AB5",
                        column: x => x.sellerId,
                        principalTable: "Account",
                        principalColumn: "accountId");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postId = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    starRating = table.Column<int>(type: "int", nullable: false),
                    feedbackUserId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    feedbackUsername = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK__Review__feedback__3D5E1FD2",
                        column: x => x.feedbackUserId,
                        principalTable: "Account",
                        principalColumn: "accountId");
                    table.ForeignKey(
                        name: "FK__Review__postId__3C69FB99",
                        column: x => x.postId,
                        principalTable: "Post",
                        principalColumn: "postId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_roleId",
                table: "Account",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeOrder_accountID",
                table: "ExchangeOrder",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeOrder_postId",
                table: "ExchangeOrder",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeOrder_receiverId",
                table: "ExchangeOrder",
                column: "receiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequest_postId",
                table: "ExchangeRequest",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequest_sellerId",
                table: "ExchangeRequest",
                column: "sellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_roleId",
                table: "Permission",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_accountId",
                table: "Post",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_categoryId",
                table: "Post",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_reportedAccountId",
                table: "Report",
                column: "reportedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_feedbackUserId",
                table: "Review",
                column: "feedbackUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_postId",
                table: "Review",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_TopUp_accountId",
                table: "TopUp",
                column: "accountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeOrder");

            migrationBuilder.DropTable(
                name: "ExchangeRequest");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "TopUp");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
