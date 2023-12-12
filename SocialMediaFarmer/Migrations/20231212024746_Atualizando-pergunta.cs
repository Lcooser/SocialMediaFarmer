using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaFarmer.Migrations
{
    public partial class Atualizandopergunta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPublicacao",
                table: "Resposta",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "ConteudoPergunta",
                table: "Pergunta",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topico",
                table: "Pergunta",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConteudoPergunta",
                table: "Pergunta");

            migrationBuilder.DropColumn(
                name: "Topico",
                table: "Pergunta");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPublicacao",
                table: "Resposta",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
