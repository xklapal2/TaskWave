using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskWave.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class GroupAndGroupMember : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Groups",
            columns: table => new
            {
                Id = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table => table.PrimaryKey("PK_Groups", x => x.Id))
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "GroupMembers",
            columns: table => new
            {
                UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                JoinedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                GroupId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GroupMembers", x => x.UserId);
                table.ForeignKey(
                    name: "FK_GroupMembers_Groups_GroupId",
                    column: x => x.GroupId,
                    principalTable: "Groups",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_GroupMembers_GroupId",
            table: "GroupMembers",
            column: "GroupId");

        migrationBuilder.CreateIndex(
            name: "IX_GroupMembers_UserId",
            table: "GroupMembers",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Groups_Name",
            table: "Groups",
            column: "Name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "GroupMembers");

        migrationBuilder.DropTable(
            name: "Groups");
    }
}