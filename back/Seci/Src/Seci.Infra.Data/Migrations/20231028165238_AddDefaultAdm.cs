using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seci.Infra.Data.Migrations
{
    public partial class AddDefaultAdm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var senha =
                "$argon2id$v=19$m=32768,t=4,p=1$8kSN61J8u9f2fBanH2sbjA$mcjis6H1GOwjNVVNBznVkOkktsa+CHUc9bP95x8IsEo";
            
            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "Id", "Nome", "Email", "Senha", "Cpf", "CriadoEm", "AtualizadoEm", "CriadoPor", "AtualizadoPor", "Ativo" },
                values: new object[,]
                {
                    { 1, "Admin", "admin@admin.com", senha, "79655469069","2022-08-21 19:05:48", "2022-08-21 19:05:48", 0, 0, true  }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .DeleteData("Administradores", "Id", 1);
        }
    }
}
