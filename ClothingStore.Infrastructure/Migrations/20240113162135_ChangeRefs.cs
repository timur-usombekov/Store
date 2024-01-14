using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRefs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Clothes_ClothisId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "ClothisId",
                table: "OrderDetails",
                newName: "ClothingVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ClothisId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ClothingVariantId");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetailId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetailId",
                table: "ClothingVariants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderDetailId",
                table: "OrderDetails",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ClothingVariants_ClothingVariantId",
                table: "OrderDetails",
                column: "ClothingVariantId",
                principalTable: "ClothingVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ClothingVariants_OrderDetailId",
                table: "OrderDetails",
                column: "OrderDetailId",
                principalTable: "ClothingVariants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ClothingVariants_ClothingVariantId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ClothingVariants_OrderDetailId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderDetailId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "ClothingVariants");

            migrationBuilder.RenameColumn(
                name: "ClothingVariantId",
                table: "OrderDetails",
                newName: "ClothisId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ClothingVariantId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ClothisId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Clothes_ClothisId",
                table: "OrderDetails",
                column: "ClothisId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
