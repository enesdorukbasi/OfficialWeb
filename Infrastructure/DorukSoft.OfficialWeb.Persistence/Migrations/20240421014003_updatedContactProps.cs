using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DorukSoft.OfficialWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatedContactProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShowedAdminMails",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowedAdminMails",
                table: "Contacts");
        }
    }
}
