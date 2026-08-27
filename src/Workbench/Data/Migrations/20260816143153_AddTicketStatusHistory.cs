using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Workbench.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketStatusChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromStatus = table.Column<string>(type: "text", nullable: false),
                    ToStatus = table.Column<string>(type: "text", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    TicketId = table.Column<int>(type: "integer", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_TicketStatusChanges_ChangedByUserId",
                table: "TicketStatusChanges",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatusChanges_TicketId",
                table: "TicketStatusChanges",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketStatusChanges");
        }
    }
}
