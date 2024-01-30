using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assignment1web.Migrations
{
    /// <inheritdoc />
    public partial class new_bid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BidderName",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1,
                columns: new[] { "Amount", "AuctionEndDate", "AuctionStartDate", "BidderName" },
                values: new object[] { 0, new DateTime(2023, 4, 8, 23, 11, 13, 410, DateTimeKind.Local).AddTicks(9305), new DateTime(2023, 4, 8, 23, 11, 13, 410, DateTimeKind.Local).AddTicks(9254), null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2,
                columns: new[] { "Amount", "AuctionEndDate", "AuctionStartDate", "BidderName" },
                values: new object[] { 0, new DateTime(2023, 4, 8, 23, 11, 13, 410, DateTimeKind.Local).AddTicks(9311), new DateTime(2023, 4, 8, 23, 11, 13, 410, DateTimeKind.Local).AddTicks(9309), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BidderName",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1,
                columns: new[] { "AuctionEndDate", "AuctionStartDate" },
                values: new object[] { new DateTime(2023, 4, 8, 21, 17, 6, 62, DateTimeKind.Local).AddTicks(7480), new DateTime(2023, 4, 8, 21, 17, 6, 62, DateTimeKind.Local).AddTicks(7437) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2,
                columns: new[] { "AuctionEndDate", "AuctionStartDate" },
                values: new object[] { new DateTime(2023, 4, 8, 21, 17, 6, 62, DateTimeKind.Local).AddTicks(7488), new DateTime(2023, 4, 8, 21, 17, 6, 62, DateTimeKind.Local).AddTicks(7486) });
        }
    }
}
