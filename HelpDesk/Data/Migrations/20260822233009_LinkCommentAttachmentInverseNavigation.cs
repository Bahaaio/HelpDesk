using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkCommentAttachmentInverseNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentAttachment_Comments_CommentId1",
                table: "CommentAttachment");

            migrationBuilder.DropIndex(
                name: "IX_CommentAttachment_CommentId1",
                table: "CommentAttachment");

            migrationBuilder.DropColumn(
                name: "CommentId1",
                table: "CommentAttachment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId1",
                table: "CommentAttachment",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentAttachment_CommentId1",
                table: "CommentAttachment",
                column: "CommentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAttachment_Comments_CommentId1",
                table: "CommentAttachment",
                column: "CommentId1",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
