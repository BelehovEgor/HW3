using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ServerWithData.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20211003170543_InitialCreate")]
    public class InitialCreate : Migration
    {
        private void CreateTableUsers(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: DbUser.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        private void CreateTableBuildings(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: DbBuilding.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    PhoneId = table.Column<Guid>(nullable: true),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Build_User",
                        column: x => x.OwnerId,
                        principalTable: DbUser.TableName,
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        private void CreateTablePhones(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: DbPhone.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    //BuildingId = table.Column<Guid>(nullable: true),
                    Number = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                });
        }



        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateTableUsers(migrationBuilder);

            CreateTableBuildings(migrationBuilder);

            CreateTablePhones(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(DbUser.TableName);

            migrationBuilder.DropTable(DbBuilding.TableName);

            migrationBuilder.DropTable(DbPhone.TableName);
        }
    }
}
