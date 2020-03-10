using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpDemo.Migrations
{
    public partial class Add_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "info_company",
                columns: table => new
                {
                    RowGuid = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: true),
                    Representative = table.Column<string>(maxLength: 50, nullable: true),
                    CapitalAmount = table.Column<int>(maxLength: 10, nullable: false),
                    EnrollDate = table.Column<DateTime>(maxLength: 20, nullable: false),
                    PhoneNum = table.Column<string>(maxLength: 15, nullable: true),
                    Industry = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    Longitude = table.Column<double>(maxLength: 20, nullable: false, defaultValue: 118.77807440799999),
                    Latitude = table.Column<double>(maxLength: 20, nullable: false, defaultValue: 32.057235501800001)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_info_company", x => x.RowGuid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "info_company");
        }
    }
}
