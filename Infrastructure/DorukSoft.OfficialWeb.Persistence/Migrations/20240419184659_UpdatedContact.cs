using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DorukSoft.OfficialWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDemands");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ProductId",
                table: "Contacts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Products_ProductId",
                table: "Contacts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Products_ProductId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ProductId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Contacts");

            migrationBuilder.CreateTable(
                name: "ProductDemands",
                columns: table => new
                {
                    ProductDemandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDemands", x => x.ProductDemandId);
                    table.ForeignKey(
                        name: "FK_ProductDemands_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDemands_ProductId",
                table: "ProductDemands",
                column: "ProductId");
        }
    }
}
