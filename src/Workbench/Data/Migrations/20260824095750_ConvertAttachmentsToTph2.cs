using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workbench.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConvertAttachmentsToTph2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentAttachment");

            migrationBuilder.DropTable(
                name: "IssueAttachment");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Attachments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Attachments",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "Attachments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CommentId",
                table: "Attachments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_IssueId",
                table: "Attachments",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Comments_CommentId",
                table: "Attachments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Issues_IssueId",
                table: "Attachments",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Comments_CommentId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Issues_IssueId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_CommentId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_IssueId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Attachments");

            migrationBuilder.CreateTable(
                name: "CommentAttachment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "integer", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentAttachment", x => new { x.CommentId, x.AttachmentId });
                    table.ForeignKey(
                        name: "FK_CommentAttachment_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentAttachment_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueAttachment",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "integer", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueAttachment", x => new { x.IssueId, x.AttachmentId });
                    table.ForeignKey(
                        name: "FK_IssueAttachment_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueAttachment_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentAttachment_AttachmentId",
                table: "CommentAttachment",
                column: "AttachmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueAttachment_AttachmentId",
                table: "IssueAttachment",
                column: "AttachmentId",
                unique: true);
        }
    }
}
