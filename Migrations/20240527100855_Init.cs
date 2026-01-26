using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kurs.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorys",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    typeAm = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L),
                    removedTypeAm = table.Column<long>(type: "INTEGER", nullable: false),
                    order = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorys", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "categorysR",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    typeAm = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L),
                    removedTypeAm = table.Column<long>(type: "INTEGER", nullable: false),
                    order = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorysR", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "types",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    order = table.Column<long>(type: "INTEGER", nullable: false),
                    categoryName = table.Column<string>(type: "TEXT", nullable: true),
                    titleAm = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types", x => x.name);
                    table.ForeignKey(
                        name: "FK_types_categorys_categoryName",
                        column: x => x.categoryName,
                        principalTable: "categorys",
                        principalColumn: "name",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "typesR",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    order = table.Column<long>(type: "INTEGER", nullable: false),
                    categoryName = table.Column<string>(type: "TEXT", nullable: true),
                    titleAm = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typesR", x => x.name);
                    table.ForeignKey(
                        name: "FK_typesR_categorysR_categoryName",
                        column: x => x.categoryName,
                        principalTable: "categorysR",
                        principalColumn: "name",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    typeNavname = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.name);
                    table.ForeignKey(
                        name: "FK_tags_types_typeNavname",
                        column: x => x.typeNavname,
                        principalTable: "types",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "titles",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<double>(type: "REAL", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    image = table.Column<string>(type: "TEXT", nullable: false, computedColumnSql: "[id] || \"-t.webp\"", stored: true),
                    typename = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_titles", x => x.id);
                    table.ForeignKey(
                        name: "FK_titles_types_typename",
                        column: x => x.typename,
                        principalTable: "types",
                        principalColumn: "name",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tagsR",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    typeNavname = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tagsR", x => x.name);
                    table.ForeignKey(
                        name: "FK_tagsR_typesR_typeNavname",
                        column: x => x.typeNavname,
                        principalTable: "typesR",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "titlesR",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<double>(type: "REAL", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    image = table.Column<string>(type: "TEXT", nullable: false, computedColumnSql: "[id] || \"-t.webp\"", stored: true),
                    typename = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_titlesR", x => x.id);
                    table.ForeignKey(
                        name: "FK_titlesR_typesR_typename",
                        column: x => x.typename,
                        principalTable: "typesR",
                        principalColumn: "name",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "descriptions",
                columns: table => new
                {
                    titleId = table.Column<long>(type: "INTEGER", nullable: false),
                    desk = table.Column<string>(type: "TEXT", nullable: false),
                    photoAm = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_descriptions", x => x.titleId);
                    table.ForeignKey(
                        name: "FK_descriptions_titles_titleId",
                        column: x => x.titleId,
                        principalTable: "titles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "descriptionsR",
                columns: table => new
                {
                    titleId = table.Column<long>(type: "INTEGER", nullable: false),
                    desk = table.Column<string>(type: "TEXT", nullable: false),
                    photoAm = table.Column<long>(type: "INTEGER", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_descriptionsR", x => x.titleId);
                    table.ForeignKey(
                        name: "FK_descriptionsR_titlesR_titleId",
                        column: x => x.titleId,
                        principalTable: "titlesR",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignedTags",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tagName = table.Column<string>(type: "TEXT", nullable: false),
                    value = table.Column<string>(type: "TEXT", nullable: false),
                    descriptiontitleId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedTags", x => x.id);
                    table.ForeignKey(
                        name: "FK_assignedTags_descriptions_descriptiontitleId",
                        column: x => x.descriptiontitleId,
                        principalTable: "descriptions",
                        principalColumn: "titleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignedTags_tags_tagName",
                        column: x => x.tagName,
                        principalTable: "tags",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignedTagsR",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tagName = table.Column<string>(type: "TEXT", nullable: false),
                    value = table.Column<string>(type: "TEXT", nullable: false),
                    descriptiontitleId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedTagsR", x => x.id);
                    table.ForeignKey(
                        name: "FK_assignedTagsR_descriptionsR_descriptiontitleId",
                        column: x => x.descriptiontitleId,
                        principalTable: "descriptionsR",
                        principalColumn: "titleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignedTagsR_tagsR_tagName",
                        column: x => x.tagName,
                        principalTable: "tagsR",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignedTags_descriptiontitleId",
                table: "assignedTags",
                column: "descriptiontitleId");

            migrationBuilder.CreateIndex(
                name: "IX_assignedTags_tagName",
                table: "assignedTags",
                column: "tagName");

            migrationBuilder.CreateIndex(
                name: "IX_assignedTagsR_descriptiontitleId",
                table: "assignedTagsR",
                column: "descriptiontitleId");

            migrationBuilder.CreateIndex(
                name: "IX_assignedTagsR_tagName",
                table: "assignedTagsR",
                column: "tagName");

            migrationBuilder.CreateIndex(
                name: "IX_categorys_order",
                table: "categorys",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categorysR_order",
                table: "categorysR",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tags_typeNavname",
                table: "tags",
                column: "typeNavname");

            migrationBuilder.CreateIndex(
                name: "IX_tagsR_typeNavname",
                table: "tagsR",
                column: "typeNavname");

            migrationBuilder.CreateIndex(
                name: "IX_titles_name",
                table: "titles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_titles_typename",
                table: "titles",
                column: "typename");

            migrationBuilder.CreateIndex(
                name: "IX_titlesR_name",
                table: "titlesR",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_titlesR_typename",
                table: "titlesR",
                column: "typename");

            migrationBuilder.CreateIndex(
                name: "IX_types_categoryName",
                table: "types",
                column: "categoryName");

            migrationBuilder.CreateIndex(
                name: "IX_types_order",
                table: "types",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_typesR_categoryName",
                table: "typesR",
                column: "categoryName");

            migrationBuilder.CreateIndex(
                name: "IX_typesR_order",
                table: "typesR",
                column: "order",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignedTags");

            migrationBuilder.DropTable(
                name: "assignedTagsR");

            migrationBuilder.DropTable(
                name: "descriptions");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "descriptionsR");

            migrationBuilder.DropTable(
                name: "tagsR");

            migrationBuilder.DropTable(
                name: "titles");

            migrationBuilder.DropTable(
                name: "titlesR");

            migrationBuilder.DropTable(
                name: "types");

            migrationBuilder.DropTable(
                name: "typesR");

            migrationBuilder.DropTable(
                name: "categorys");

            migrationBuilder.DropTable(
                name: "categorysR");
        }
    }
}
