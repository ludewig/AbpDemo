using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpDemo.Migrations
{
    public partial class Add_GoodsRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INFO_GoodsRecord",
                columns: table => new
                {
                    RowGuid = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    GoodsName = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "默认值"),
                    GoodsType = table.Column<string>(maxLength: 50, nullable: true),
                    Location = table.Column<string>(maxLength: 50, nullable: true),
                    OperateType = table.Column<int>(maxLength: 10, nullable: false),
                    GoodsNum = table.Column<int>(maxLength: 10, nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INFO_GoodsRecord", x => x.RowGuid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INFO_GoodsRecord");
        }
    }
}
