using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DorukSoft.OfficialWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCompanyInfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CompanyInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CompanyInformations",
                keyColumn: "CompanyInformationId",
                keyValue: 1,
                column: "ImageUrl",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CompanyInformations");
        }
    }
}
