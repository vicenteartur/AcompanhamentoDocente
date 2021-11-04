using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcompanhamentoDocente.Migrations
{
    public partial class Acomp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbCargo",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cargo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NiveldeAcesso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkCargo", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "tbClassificacaoCriterio",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Classificacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkClassificacaoCriterio", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "tbEstado",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sigla = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkEstado", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "tbModalidade",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modalidade = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbModalidade", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbColaborador",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nchar(80)", fixedLength: true, maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "nchar(80)", fixedLength: true, maxLength: 80, nullable: false),
                    Senha = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    CodigoCargo = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkColaborador", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCodigoCargo",
                        column: x => x.CodigoCargo,
                        principalTable: "tbCargo",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbCriterioAvaliacao",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Criterio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CodigoClassificacaoCriterio = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkCriterioAvaliacao", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCodigoClassificacaoCriterio",
                        column: x => x.CodigoClassificacaoCriterio,
                        principalTable: "tbClassificacaoCriterio",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbCidade",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodigoEstado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkCidade", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCodigoEstado",
                        column: x => x.CodigoEstado,
                        principalTable: "tbEstado",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbAno",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Turma = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    CodigoModalidade = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkAno", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_tbAno_tbModalidade",
                        column: x => x.CodigoModalidade,
                        principalTable: "tbModalidade",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbComponenteCurricular",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponenteCurricular = table.Column<string>(type: "nchar(25)", fixedLength: true, maxLength: 25, nullable: false),
                    SubArea = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    CodigoModalidade = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkComponenteCurricular", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCodigoModalidade",
                        column: x => x.CodigoModalidade,
                        principalTable: "tbModalidade",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbEscola",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Escola = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodigoCidade = table.Column<int>(type: "int", nullable: false),
                    INEP = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkEscola", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCodigoCidade",
                        column: x => x.CodigoCidade,
                        principalTable: "tbCidade",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbCriterioComponenteCurricular",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCriterioAvaliacao = table.Column<int>(type: "int", nullable: false),
                    CodigoComponenteCurricular = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCriterioComponenteCurricular", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCodigoCAvaliacao",
                        column: x => x.CodigoCriterioAvaliacao,
                        principalTable: "tbCriterioAvaliacao",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkCodigoCCurricular",
                        column: x => x.CodigoComponenteCurricular,
                        principalTable: "tbComponenteCurricular",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbAtribuicaoColaboradorEscola",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoEscola = table.Column<int>(type: "int", nullable: false),
                    CodigoColaborador = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkAtribuicaoColaboradorEscola", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCodigoColaborador",
                        column: x => x.CodigoColaborador,
                        principalTable: "tbColaborador",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fkCodigoEscola",
                        column: x => x.CodigoEscola,
                        principalTable: "tbEscola",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbAtribuicaoComponenteCurricularAnoColaboradorEscola",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoAtribuicaoColaboradorEscola = table.Column<int>(type: "int", nullable: false),
                    CodigoComponenteCurricular = table.Column<int>(type: "int", nullable: false),
                    CodigoAno = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkAtribuicaoComponenteCurricularAnoColaboradorEscola", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_dbo.tbAtribuicaoComponenteCurricularAnoColaboradorEscola_dbo.tbAtribuicaoColaboradorEscola_CodigoAtribuicaoColaboradorEscola",
                        column: x => x.CodigoAtribuicaoColaboradorEscola,
                        principalTable: "tbAtribuicaoColaboradorEscola",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkCodigoAno",
                        column: x => x.CodigoAno,
                        principalTable: "tbAno",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkCodigoComponenteCurricular",
                        column: x => x.CodigoComponenteCurricular,
                        principalTable: "tbComponenteCurricular",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbAvaliacao",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datarealizacao = table.Column<DateTime>(type: "date", nullable: false),
                    CodigoColaboradorAvaliador = table.Column<int>(type: "int", nullable: false),
                    CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola = table.Column<int>(type: "int", nullable: false),
                    Finalizada = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkAvaliacao", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkAtribuicaoComponenteCurricularAnoColaboradorEscola",
                        column: x => x.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola,
                        principalTable: "tbAtribuicaoComponenteCurricularAnoColaboradorEscola",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkCodigoColaboradorAvaliador",
                        column: x => x.CodigoColaboradorAvaliador,
                        principalTable: "tbColaborador",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbCriterioAvaliado",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCriterioAvaliacao = table.Column<int>(type: "int", nullable: false),
                    Conceito = table.Column<byte>(type: "tinyint", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    CodigoAvaliacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkCriterioAvaliado", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCodigoAvaliacao",
                        column: x => x.CodigoAvaliacao,
                        principalTable: "tbAvaliacao",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fkCodigoCriterioAvaliacao",
                        column: x => x.CodigoCriterioAvaliacao,
                        principalTable: "tbCriterioAvaliacao",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbAno_CodigoModalidade",
                table: "tbAno",
                column: "CodigoModalidade");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoColaborador",
                table: "tbAtribuicaoColaboradorEscola",
                column: "CodigoColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoEscola",
                table: "tbAtribuicaoColaboradorEscola",
                column: "CodigoEscola");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoAno",
                table: "tbAtribuicaoComponenteCurricularAnoColaboradorEscola",
                column: "CodigoAno");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoAtribuicaoColaboradorEscola",
                table: "tbAtribuicaoComponenteCurricularAnoColaboradorEscola",
                column: "CodigoAtribuicaoColaboradorEscola");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoComponenteCurricular",
                table: "tbAtribuicaoComponenteCurricularAnoColaboradorEscola",
                column: "CodigoComponenteCurricular");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoColaboradorAvaliador",
                table: "tbAvaliacao",
                column: "CodigoColaboradorAvaliador");

            migrationBuilder.CreateIndex(
                name: "IX_tbAvaliacao_CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola",
                table: "tbAvaliacao",
                column: "CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoEstado",
                table: "tbCidade",
                column: "CodigoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoCargo",
                table: "tbColaborador",
                column: "CodigoCargo");

            migrationBuilder.CreateIndex(
                name: "IX_tbComponenteCurricular_CodigoModalidade",
                table: "tbComponenteCurricular",
                column: "CodigoModalidade");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoClassificacaoCriterio",
                table: "tbCriterioAvaliacao",
                column: "CodigoClassificacaoCriterio");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoAvaliacao",
                table: "tbCriterioAvaliado",
                column: "CodigoAvaliacao");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoCriterioAvaliacao",
                table: "tbCriterioAvaliado",
                column: "CodigoCriterioAvaliacao");

            migrationBuilder.CreateIndex(
                name: "IX_tbCriterioComponenteCurricular_CodigoComponenteCurricular",
                table: "tbCriterioComponenteCurricular",
                column: "CodigoComponenteCurricular");

            migrationBuilder.CreateIndex(
                name: "IX_tbCriterioComponenteCurricular_CodigoCriterioAvaliacao",
                table: "tbCriterioComponenteCurricular",
                column: "CodigoCriterioAvaliacao");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoCidade",
                table: "tbEscola",
                column: "CodigoCidade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tbCriterioAvaliado");

            migrationBuilder.DropTable(
                name: "tbCriterioComponenteCurricular");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tbAvaliacao");

            migrationBuilder.DropTable(
                name: "tbCriterioAvaliacao");

            migrationBuilder.DropTable(
                name: "tbAtribuicaoComponenteCurricularAnoColaboradorEscola");

            migrationBuilder.DropTable(
                name: "tbClassificacaoCriterio");

            migrationBuilder.DropTable(
                name: "tbAtribuicaoColaboradorEscola");

            migrationBuilder.DropTable(
                name: "tbAno");

            migrationBuilder.DropTable(
                name: "tbComponenteCurricular");

            migrationBuilder.DropTable(
                name: "tbColaborador");

            migrationBuilder.DropTable(
                name: "tbEscola");

            migrationBuilder.DropTable(
                name: "tbModalidade");

            migrationBuilder.DropTable(
                name: "tbCargo");

            migrationBuilder.DropTable(
                name: "tbCidade");

            migrationBuilder.DropTable(
                name: "tbEstado");
        }
    }
}
