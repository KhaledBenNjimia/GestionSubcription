﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionSubcription.Migrations
{
    /// <inheritdoc />
    public partial class AddLicenseKeyToSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicenseKey",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseKey",
                table: "Subscriptions");
        }
    }
}
