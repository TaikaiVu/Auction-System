using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assignment1web.Migrations
{
    /// <inheritdoc />
    public partial class bid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1,
                columns: new[] { "AuctionEndDate", "AuctionStartDate" },
                values: new object[] { new DateTime(2023, 3, 1, 1, 24, 20, 897, DateTimeKind.Local).AddTicks(5160), new DateTime(2023, 3, 1, 1, 24, 20, 897, DateTimeKind.Local).AddTicks(5116) });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2,
                columns: new[] { "AuctionEndDate", "AuctionStartDate" },
                values: new object[] { new DateTime(2023, 3, 1, 1, 24, 20, 897, DateTimeKind.Local).AddTicks(5166), new DateTime(2023, 3, 1, 1, 24, 20, 897, DateTimeKind.Local).AddTicks(5165) });
        }
    }
}
