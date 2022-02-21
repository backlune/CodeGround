using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeGround.Modeling.Migrations
{
    public partial class AddRecurringDependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TemplateInvoiceId",
                table: "RecurringInvoices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecurringInvoiceId",
                table: "Invoices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecurringInvoices_TemplateInvoiceId",
                table: "RecurringInvoices",
                column: "TemplateInvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_RecurringInvoiceId",
                table: "Invoices",
                column: "RecurringInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_RecurringInvoices_RecurringInvoiceId",
                table: "Invoices",
                column: "RecurringInvoiceId",
                principalTable: "RecurringInvoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringInvoices_Invoices_TemplateInvoiceId",
                table: "RecurringInvoices",
                column: "TemplateInvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_RecurringInvoices_RecurringInvoiceId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringInvoices_Invoices_TemplateInvoiceId",
                table: "RecurringInvoices");

            migrationBuilder.DropIndex(
                name: "IX_RecurringInvoices_TemplateInvoiceId",
                table: "RecurringInvoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_RecurringInvoiceId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TemplateInvoiceId",
                table: "RecurringInvoices");

            migrationBuilder.DropColumn(
                name: "RecurringInvoiceId",
                table: "Invoices");
        }
    }
}
