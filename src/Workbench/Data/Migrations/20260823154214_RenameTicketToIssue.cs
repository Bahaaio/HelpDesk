using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Workbench.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTicketToIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tickets_TicketId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Tickets_TicketId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "TagTicket");

            migrationBuilder.DropTable(
                name: "TicketAttachment");

            migrationBuilder.DropTable(
                name: "TicketStatusChanges");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Votes",
                newName: "IssueId");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Comments",
                newName: "IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TicketId",
                table: "Comments",
                newName: "IX_Comments_IssueId");

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    AssignedToId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Issues_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
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

            migrationBuilder.CreateTable(
                name: "IssueStatusChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromStatus = table.Column<string>(type: "text", nullable: false),
                    ToStatus = table.Column<string>(type: "text", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IssueId = table.Column<int>(type: "integer", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatusChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueStatusChanges_AspNetUsers_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueStatusChanges_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueTag",
                columns: table => new
                {
                    IssuesId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueTag", x => new { x.IssuesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_IssueTag_Issues_IssuesId",
                        column: x => x.IssuesId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueAttachment_AttachmentId",
                table: "IssueAttachment",
                column: "AttachmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedToId",
                table: "Issues",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AuthorId",
                table: "Issues",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueStatusChanges_ChangedByUserId",
                table: "IssueStatusChanges",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueStatusChanges_IssueId",
                table: "IssueStatusChanges",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTag_TagsId",
                table: "IssueTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Issues_IssueId",
                table: "Comments",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Issues_IssueId",
                table: "Votes",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Issues_IssueId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Issues_IssueId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "IssueAttachment");

            migrationBuilder.DropTable(
                name: "IssueStatusChanges");

            migrationBuilder.DropTable(
                name: "IssueTag");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Votes",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Comments",
                newName: "TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_IssueId",
                table: "Comments",
                newName: "IX_Comments_TicketId");

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssignedToId = table.Column<int>(type: "integer", nullable: true),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagTicket",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "integer", nullable: false),
                    TicketsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTicket", x => new { x.TagsId, x.TicketsId });
                    table.ForeignKey(
                        name: "FK_TagTicket_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTicket_Tickets_TicketsId",
                        column: x => x.TicketsId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketAttachment",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "integer", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAttachment", x => new { x.TicketId, x.AttachmentId });
                    table.ForeignKey(
                        name: "FK_TicketAttachment_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketAttachment_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatusChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChangedByUserId = table.Column<int>(type: "integer", nullable: false),
                    TicketId = table.Column<int>(type: "integer", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FromStatus = table.Column<string>(type: "text", nullable: false),
                    ToStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatusChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketStatusChanges_AspNetUsers_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketStatusChanges_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTicket_TicketsId",
                table: "TagTicket",
                column: "TicketsId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachment_AttachmentId",
                table: "TicketAttachment",
                column: "AttachmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedToId",
                table: "Issues",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AuthorId",
                table: "Issues",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatusChanges_ChangedByUserId",
                table: "TicketStatusChanges",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatusChanges_TicketId",
                table: "TicketStatusChanges",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tickets_TicketId",
                table: "Comments",
                column: "TicketId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Tickets_TicketId",
                table: "Votes",
                column: "TicketId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
