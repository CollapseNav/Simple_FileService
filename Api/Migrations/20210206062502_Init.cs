using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Ext = table.Column<string>(type: "TEXT", nullable: true),
                    Icon = table.Column<string>(type: "TEXT", nullable: true),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dirs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    MapPath = table.Column<string>(type: "TEXT", nullable: true),
                    Sys = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Size = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TypeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsVisible = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ext = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Dirs_FileTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: true),
                    MapPath = table.Column<string>(type: "TEXT", nullable: true),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Sys = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Size = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TypeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsVisible = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ext = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Dirs_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Dirs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_FileTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FileTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Dirs",
                columns: new[] { "Id", "AddTime", "Ext", "FileName", "IsVisible", "MapPath", "ParentId", "Size", "Sys", "TypeId" },
                values: new object[] { new Guid("ca2ee4ba-b4db-4f06-99f6-98df27952663"), new DateTime(2021, 2, 6, 14, 25, 2, 145, DateTimeKind.Local).AddTicks(7123), null, "root", true, "", null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Dirs_ParentId",
                table: "Dirs",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dirs_TypeId",
                table: "Dirs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ParentId",
                table: "Files",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_TypeId",
                table: "Files",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Dirs");

            migrationBuilder.DropTable(
                name: "FileTypes");
        }
    }
}
