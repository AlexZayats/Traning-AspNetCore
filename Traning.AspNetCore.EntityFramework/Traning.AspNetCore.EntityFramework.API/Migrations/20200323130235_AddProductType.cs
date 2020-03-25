using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traning.AspNetCore.EntityFramework.API.Migrations
{
    public partial class AddProductType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews");

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductReviews",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Products");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductReviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
