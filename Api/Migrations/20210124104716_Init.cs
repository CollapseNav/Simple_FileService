using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dirs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: true),
                    MapPath = table.Column<string>(nullable: true),
                    Sys = table.Column<string>(maxLength: 20, nullable: true),
                    Size = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    IsVisible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dirs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dirs_Dirs_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Dirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Ext = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: true),
                    Sys = table.Column<string>(maxLength: 20, nullable: true),
                    Size = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    IsVisible = table.Column<bool>(nullable: false),
                    Ext = table.Column<string>(maxLength: 20, nullable: true),
                    TypeId = table.Column<Guid>(nullable: true),
                    FileTypeId = table.Column<Guid>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    MapPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_FileTypes_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Dirs_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Dirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Dirs",
                columns: new[] { "Id", "AddTime", "FileName", "IsVisible", "MapPath", "ParentId", "Size", "Sys" },
                values: new object[] { new Guid("a32bf347-3472-4a59-af9c-6241c2e33dc3"), new DateTime(2021, 1, 24, 18, 47, 16, 756, DateTimeKind.Local).AddTicks(1947), "root", true, "", null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Dirs_ParentId",
                table: "Dirs",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileTypeId",
                table: "Files",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ParentId",
                table: "Files",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "FileTypes");

            migrationBuilder.DropTable(
                name: "Dirs");
        }
    }
}
