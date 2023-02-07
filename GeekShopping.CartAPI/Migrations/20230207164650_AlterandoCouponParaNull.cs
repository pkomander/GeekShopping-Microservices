using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CartAPI.Migrations
{
    public partial class AlterandoCouponParaNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_cart_header_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_product_ProductId",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails");

            migrationBuilder.RenameTable(
                name: "CartDetails",
                newName: "cart_detail");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_ProductId",
                table: "cart_detail",
                newName: "IX_cart_detail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CartHeaderId",
                table: "cart_detail",
                newName: "IX_cart_detail_CartHeaderId");

            migrationBuilder.AlterColumn<string>(
                name: "coupon_code",
                table: "cart_header",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart_detail",
                table: "cart_detail",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_detail_cart_header_CartHeaderId",
                table: "cart_detail",
                column: "CartHeaderId",
                principalTable: "cart_header",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_detail_product_ProductId",
                table: "cart_detail",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_detail_cart_header_CartHeaderId",
                table: "cart_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_detail_product_ProductId",
                table: "cart_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart_detail",
                table: "cart_detail");

            migrationBuilder.RenameTable(
                name: "cart_detail",
                newName: "CartDetails");

            migrationBuilder.RenameIndex(
                name: "IX_cart_detail_ProductId",
                table: "CartDetails",
                newName: "IX_CartDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_cart_detail_CartHeaderId",
                table: "CartDetails",
                newName: "IX_CartDetails_CartHeaderId");

            migrationBuilder.AlterColumn<string>(
                name: "coupon_code",
                table: "cart_header",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_cart_header_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "cart_header",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_product_ProductId",
                table: "CartDetails",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
