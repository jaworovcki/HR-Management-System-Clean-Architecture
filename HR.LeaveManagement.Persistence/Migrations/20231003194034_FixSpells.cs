using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixSpells : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeberOfDays",
                table: "LeaveAllocations",
                newName: "NumberOfDays");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 10, 3, 22, 40, 34, 806, DateTimeKind.Local).AddTicks(9874), new DateTime(2023, 10, 3, 22, 40, 34, 806, DateTimeKind.Local).AddTicks(9937) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfDays",
                table: "LeaveAllocations",
                newName: "NumeberOfDays");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 10, 2, 17, 31, 29, 545, DateTimeKind.Local).AddTicks(765), new DateTime(2023, 10, 2, 17, 31, 29, 545, DateTimeKind.Local).AddTicks(821) });
        }
    }
}
