using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20211103113251_CreateLinkBuildUser")]
    public class CreateLinkBuildUser : Migration
    {
        private void CreateLinkTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkTable",
                columns: table => new
                {
                    BuildingId = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false)
                });
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateLinkTable(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("LinkTable");
        }
    }
}
