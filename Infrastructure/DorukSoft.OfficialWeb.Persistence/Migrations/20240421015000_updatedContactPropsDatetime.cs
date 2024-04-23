using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DorukSoft.OfficialWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatedContactPropsDatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Contacts");
        }
    }
}
