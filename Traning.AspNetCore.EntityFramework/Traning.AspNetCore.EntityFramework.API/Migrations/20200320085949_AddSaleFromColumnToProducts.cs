using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traning.AspNetCore.EntityFramework.API.Migrations
{
    public partial class AddSaleFromColumnToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SaleFrom",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleFrom",
                table: "Products");
        }
    }
}
