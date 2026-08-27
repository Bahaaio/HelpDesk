using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workbench.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConvertAttachmentsToTph : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "Attachments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Attachments",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "TicketAttachment"); // default for already existing records
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Attachments");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "Attachments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
