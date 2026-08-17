using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachmentJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // create join table first
            migrationBuilder.CreateTable(
                name: "TicketAttachment",
                columns: table => new
                {
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAttachment", x => new { x.OwnerId, x.AttachmentId });
                    table.ForeignKey(
                        name: "FK_TicketAttachment_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketAttachment_Tickets_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            // migrate data
            migrationBuilder.Sql(@"
                INSERT INTO ""TicketAttachment"" (""OwnerId"", ""AttachmentId"") 
                SELECT ""TicketId"", ""Id""
                FROM ""Attachments""
                WHERE ""TicketId"" IS NOT NULL
            ");
            
            // drop old columns
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Tickets_TicketId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_TicketId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Attachments");


            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachment_AttachmentId",
                table: "TicketAttachment",
                column: "AttachmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketAttachment");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Attachments",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Attachments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TicketId",
                table: "Attachments",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Tickets_TicketId",
                table: "Attachments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
