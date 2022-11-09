using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    userId = table.Column<Guid>(nullable: false),
                    fullName = table.Column<string>(maxLength: 50, nullable: true),
                    userName = table.Column<string>(maxLength: 50, nullable: true),
                    address = table.Column<string>(nullable: true),
                    state = table.Column<int>(nullable: false),
                    decentralization = table.Column<bool>(name: "decentralization ", nullable: false),
                    phone = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    birtday = table.Column<DateTime>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    counterror = table.Column<int>(nullable: false),
                    islock = table.Column<bool>(nullable: false),
                    timelock = table.Column<DateTime>(nullable: true),
                    token_change_password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    id_brand = table.Column<Guid>(nullable: false),
                    brand_name = table.Column<string>(maxLength: 50, nullable: true),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brand", x => x.id_brand);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    category_id = table.Column<Guid>(nullable: false),
                    category_name = table.Column<string>(maxLength: 50, nullable: true),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "certify",
                columns: table => new
                {
                    id_certify = table.Column<Guid>(nullable: false),
                    certify_type = table.Column<string>(maxLength: 50, nullable: true),
                    image_certify = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certify", x => x.id_certify);
                });

            migrationBuilder.CreateTable(
                name: "dimqlty_mst",
                columns: table => new
                {
                    dimqlty_id = table.Column<Guid>(nullable: false),
                    dim_qlty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dimqlty_mst", x => x.dimqlty_id);
                });

            migrationBuilder.CreateTable(
                name: "dimqlty_submst",
                columns: table => new
                {
                    dimsub_type_id = table.Column<Guid>(nullable: false),
                    dimqlty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dimqlty_submst", x => x.dimsub_type_id);
                });

            migrationBuilder.CreateTable(
                name: "goldk",
                columns: table => new
                {
                    goldtype_id = table.Column<Guid>(nullable: false),
                    gold_crt = table.Column<string>(nullable: true),
                    gold_rate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goldk", x => x.goldtype_id);
                });

            migrationBuilder.CreateTable(
                name: "stone",
                columns: table => new
                {
                    stoneqlty_id = table.Column<Guid>(nullable: false),
                    stoneqlty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stone", x => x.stoneqlty_id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id_order = table.Column<Guid>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    phone = table.Column<int>(nullable: false),
                    total = table.Column<float>(nullable: false),
                    note = table.Column<string>(nullable: true),
                    booking_date = table.Column<DateTime>(nullable: true),
                    cancellation_date = table.Column<DateTime>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    account_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id_order);
                    table.ForeignKey(
                        name: "FK_Order_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cate_subcategory",
                columns: table => new
                {
                    id_subcategory = table.Column<Guid>(nullable: false),
                    subcategory_name = table.Column<string>(maxLength: 50, nullable: true),
                    status = table.Column<bool>(nullable: false),
                    category_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cate_subcategory", x => x.id_subcategory);
                    table.ForeignKey(
                        name: "FK_cate_subcategory_Category_category_id",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    product_name = table.Column<string>(maxLength: 50, nullable: true),
                    quantity = table.Column<int>(nullable: false),
                    total = table.Column<float>(nullable: false),
                    image = table.Column<string>(nullable: true),
                    booking_date = table.Column<DateTime>(nullable: true),
                    cancellation_date = table.Column<DateTime>(nullable: true),
                    status = table.Column<bool>(nullable: false),
                    order_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_order_id",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id_order",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dimmst",
                columns: table => new
                {
                    style_code = table.Column<string>(nullable: false),
                    dimmst_name = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<string>(nullable: true),
                    dim_crt = table.Column<float>(nullable: false),
                    dim_pcs = table.Column<float>(nullable: false),
                    dim_gm = table.Column<float>(nullable: false),
                    dim_size = table.Column<int>(nullable: false),
                    detail = table.Column<string>(nullable: true),
                    dim_amt = table.Column<float>(nullable: false),
                    category_id = table.Column<Guid>(nullable: false),
                    subcategory_id = table.Column<Guid>(nullable: true),
                    certify_id = table.Column<Guid>(nullable: false),
                    dimqlty_id = table.Column<Guid>(nullable: false),
                    dimsubtype_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dimmst", x => x.style_code);
                    table.ForeignKey(
                        name: "FK_dimmst_Category_category_id",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dimmst_certify_certify_id",
                        column: x => x.certify_id,
                        principalTable: "certify",
                        principalColumn: "id_certify",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dimmst_dimqlty_mst_dimqlty_id",
                        column: x => x.dimqlty_id,
                        principalTable: "dimqlty_mst",
                        principalColumn: "dimqlty_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dimmst_dimqlty_submst_dimsubtype_id",
                        column: x => x.dimsubtype_id,
                        principalTable: "dimqlty_submst",
                        principalColumn: "dimsub_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dimmst_cate_subcategory_subcategory_id",
                        column: x => x.subcategory_id,
                        principalTable: "cate_subcategory",
                        principalColumn: "id_subcategory",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "item_product",
                columns: table => new
                {
                    style_code = table.Column<string>(nullable: false),
                    pairs = table.Column<int>(nullable: false),
                    product_name = table.Column<string>(maxLength: 150, nullable: true),
                    quantity = table.Column<int>(nullable: false),
                    image_product = table.Column<string>(nullable: true),
                    pro_quality = table.Column<int>(nullable: false),
                    gold_wt = table.Column<float>(nullable: false),
                    stone_wt = table.Column<float>(nullable: false),
                    net_gold = table.Column<float>(nullable: false),
                    total_gross_wt = table.Column<float>(nullable: false),
                    gold_rate = table.Column<float>(nullable: false),
                    gold_amt = table.Column<float>(nullable: false),
                    gold_making = table.Column<float>(nullable: true),
                    stone_making = table.Column<float>(nullable: true),
                    other_making = table.Column<float>(nullable: false),
                    total_making = table.Column<float>(nullable: false),
                    detail = table.Column<string>(nullable: true),
                    mrp = table.Column<string>(nullable: true),
                    category_id = table.Column<Guid>(nullable: false),
                    brand_id = table.Column<Guid>(nullable: false),
                    certify_id = table.Column<Guid>(nullable: false),
                    subcategory_id = table.Column<Guid>(nullable: true),
                    goldtype_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_product", x => x.style_code);
                    table.ForeignKey(
                        name: "FK_item_product_brand_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brand",
                        principalColumn: "id_brand",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_product_Category_category_id",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_product_certify_certify_id",
                        column: x => x.certify_id,
                        principalTable: "certify",
                        principalColumn: "id_certify",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_product_goldk_goldtype_id",
                        column: x => x.goldtype_id,
                        principalTable: "goldk",
                        principalColumn: "goldtype_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_product_cate_subcategory_subcategory_id",
                        column: x => x.subcategory_id,
                        principalTable: "cate_subcategory",
                        principalColumn: "id_subcategory",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stone_mst",
                columns: table => new
                {
                    style_code = table.Column<string>(nullable: false),
                    stone_name = table.Column<string>(maxLength: 150, nullable: true),
                    image = table.Column<string>(nullable: true),
                    stone_gm = table.Column<float>(nullable: false),
                    stone_pcs = table.Column<float>(nullable: false),
                    stone_crt = table.Column<float>(nullable: false),
                    stone_rate = table.Column<float>(nullable: false),
                    stone_amt = table.Column<float>(nullable: false),
                    detail = table.Column<string>(nullable: true),
                    category_id = table.Column<Guid>(nullable: false),
                    subcategory_id = table.Column<Guid>(nullable: true),
                    stoneqlty_id = table.Column<Guid>(nullable: false),
                    certify_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stone_mst", x => x.style_code);
                    table.ForeignKey(
                        name: "FK_stone_mst_Category_category_id",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stone_mst_certify_certify_id",
                        column: x => x.certify_id,
                        principalTable: "certify",
                        principalColumn: "id_certify",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stone_mst_stone_stoneqlty_id",
                        column: x => x.stoneqlty_id,
                        principalTable: "stone",
                        principalColumn: "stoneqlty_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stone_mst_cate_subcategory_subcategory_id",
                        column: x => x.subcategory_id,
                        principalTable: "cate_subcategory",
                        principalColumn: "id_subcategory",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    content_comment = table.Column<string>(nullable: true),
                    feedback = table.Column<int>(nullable: false),
                    account_id = table.Column<Guid>(nullable: false),
                    style_code_itemproduct = table.Column<string>(nullable: true),
                    style_code_stone_mst = table.Column<string>(nullable: true),
                    style_code_dim_mst = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_comment_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_dimmst_style_code_dim_mst",
                        column: x => x.style_code_dim_mst,
                        principalTable: "dimmst",
                        principalColumn: "style_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comment_item_product_style_code_itemproduct",
                        column: x => x.style_code_itemproduct,
                        principalTable: "item_product",
                        principalColumn: "style_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comment_stone_mst_style_code_stone_mst",
                        column: x => x.style_code_stone_mst,
                        principalTable: "stone_mst",
                        principalColumn: "style_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "image_product",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    image = table.Column<string>(nullable: true),
                    style_code_itemproduct = table.Column<string>(nullable: true),
                    style_code_stone_mst = table.Column<string>(nullable: true),
                    style_code_dim_mst = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_image_product_dimmst_style_code_dim_mst",
                        column: x => x.style_code_dim_mst,
                        principalTable: "dimmst",
                        principalColumn: "style_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_image_product_item_product_style_code_itemproduct",
                        column: x => x.style_code_itemproduct,
                        principalTable: "item_product",
                        principalColumn: "style_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_image_product_stone_mst_style_code_stone_mst",
                        column: x => x.style_code_stone_mst,
                        principalTable: "stone_mst",
                        principalColumn: "style_code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cate_subcategory_category_id",
                table: "cate_subcategory",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_account_id",
                table: "comment",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_style_code_dim_mst",
                table: "comment",
                column: "style_code_dim_mst");

            migrationBuilder.CreateIndex(
                name: "IX_comment_style_code_itemproduct",
                table: "comment",
                column: "style_code_itemproduct");

            migrationBuilder.CreateIndex(
                name: "IX_comment_style_code_stone_mst",
                table: "comment",
                column: "style_code_stone_mst");

            migrationBuilder.CreateIndex(
                name: "IX_dimmst_category_id",
                table: "dimmst",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_dimmst_certify_id",
                table: "dimmst",
                column: "certify_id");

            migrationBuilder.CreateIndex(
                name: "IX_dimmst_dimqlty_id",
                table: "dimmst",
                column: "dimqlty_id");

            migrationBuilder.CreateIndex(
                name: "IX_dimmst_dimsubtype_id",
                table: "dimmst",
                column: "dimsubtype_id");

            migrationBuilder.CreateIndex(
                name: "IX_dimmst_subcategory_id",
                table: "dimmst",
                column: "subcategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_product_style_code_dim_mst",
                table: "image_product",
                column: "style_code_dim_mst");

            migrationBuilder.CreateIndex(
                name: "IX_image_product_style_code_itemproduct",
                table: "image_product",
                column: "style_code_itemproduct");

            migrationBuilder.CreateIndex(
                name: "IX_image_product_style_code_stone_mst",
                table: "image_product",
                column: "style_code_stone_mst");

            migrationBuilder.CreateIndex(
                name: "IX_item_product_brand_id",
                table: "item_product",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_product_category_id",
                table: "item_product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_product_certify_id",
                table: "item_product",
                column: "certify_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_product_goldtype_id",
                table: "item_product",
                column: "goldtype_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_product_subcategory_id",
                table: "item_product",
                column: "subcategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_account_id",
                table: "Order",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_order_id",
                table: "OrderDetail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_stone_mst_category_id",
                table: "stone_mst",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_stone_mst_certify_id",
                table: "stone_mst",
                column: "certify_id");

            migrationBuilder.CreateIndex(
                name: "IX_stone_mst_stoneqlty_id",
                table: "stone_mst",
                column: "stoneqlty_id");

            migrationBuilder.CreateIndex(
                name: "IX_stone_mst_subcategory_id",
                table: "stone_mst",
                column: "subcategory_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "image_product");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "dimmst");

            migrationBuilder.DropTable(
                name: "item_product");

            migrationBuilder.DropTable(
                name: "stone_mst");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "dimqlty_mst");

            migrationBuilder.DropTable(
                name: "dimqlty_submst");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "goldk");

            migrationBuilder.DropTable(
                name: "certify");

            migrationBuilder.DropTable(
                name: "stone");

            migrationBuilder.DropTable(
                name: "cate_subcategory");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
