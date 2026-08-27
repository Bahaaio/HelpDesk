using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workbench.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameOwnerIdToTicketId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketAttachment_Tickets_OwnerId",
                table: "TicketAttachment");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "TicketAttachment",
                newName: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAttachment_Tickets_TicketId",
                table: "TicketAttachment",
                column: "TicketId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketAttachment_Tickets_TicketId",
                table: "TicketAttachment");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "TicketAttachment",
                newName: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAttachment_Tickets_OwnerId",
                table: "TicketAttachment",
                column: "OwnerId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
