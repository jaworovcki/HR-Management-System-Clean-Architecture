using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixTheSpells : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequstComment",
                table: "LeaveRequests",
                newName: "RequestComment");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 10, 7, 18, 19, 54, 672, DateTimeKind.Local).AddTicks(301), new DateTime(2023, 10, 7, 18, 19, 54, 672, DateTimeKind.Local).AddTicks(378) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestComment",
                table: "LeaveRequests",
                newName: "RequstComment");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 10, 3, 22, 40, 34, 806, DateTimeKind.Local).AddTicks(9874), new DateTime(2023, 10, 3, 22, 40, 34, 806, DateTimeKind.Local).AddTicks(9937) });
        }
    }
}
