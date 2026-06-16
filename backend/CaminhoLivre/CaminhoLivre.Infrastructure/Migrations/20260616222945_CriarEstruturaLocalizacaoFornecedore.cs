using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CaminhoLivre.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriarEstruturaLocalizacaoFornecedore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sigla = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "municipios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    codigo_ibge = table.Column<long>(type: "bigint", nullable: false),
                    EstadoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipios", x => x.id);
                    table.ForeignKey(
                        name: "fk_municipios_estados_estado_id",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logradouros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cep = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Rua = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    MunicipioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logradouros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logradouros_municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_fantasia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    razao_social = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    cnpj_cpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    telefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    logradouro_id = table.Column<long>(type: "bigint", nullable: false),
                    numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    data_cadastro = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores", x => x.id);
                    table.ForeignKey(
                        name: "fk_fornecedores_logradouros_logradouro_id",
                        column: x => x.logradouro_id,
                        principalTable: "Logradouros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_fornecedores_cnpj_cpf",
                table: "fornecedores",
                column: "cnpj_cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_logradouro_id",
                table: "fornecedores",
                column: "logradouro_id");

            migrationBuilder.CreateIndex(
                name: "IX_Logradouros_Cep",
                table: "Logradouros",
                column: "Cep",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logradouros_MunicipioId",
                table: "Logradouros",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_municipios_codigo_ibge",
                table: "municipios",
                column: "codigo_ibge",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipios_EstadoId",
                table: "municipios",
                column: "EstadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fornecedores");

            migrationBuilder.DropTable(
                name: "Logradouros");

            migrationBuilder.DropTable(
                name: "municipios");

            migrationBuilder.DropTable(
                name: "estados");
        }
    }
}
