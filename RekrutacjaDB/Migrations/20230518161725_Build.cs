using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rekrutacja.Migrations
{
    /// <inheritdoc />
    public partial class Build : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kierunki",
                columns: table => new
                {
                    kierunek_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    opis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    stopień_studiów = table.Column<int>(type: "int", nullable: false),
                    tryb_studiów = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    liczba_miejsc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kierunki", x => x.kierunek_id);
                });

            migrationBuilder.CreateTable(
                name: "Użytkownicy",
                columns: table => new
                {
                    użytkownik_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imię = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nr_dowodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    płeć = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    data_urodzenia = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Użytkownicy", x => x.użytkownik_id);
                });

            migrationBuilder.CreateTable(
                name: "Adresy",
                columns: table => new
                {
                    adres_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    użytkownik_id = table.Column<int>(type: "int", nullable: true),
                    państwo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    miasto = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ulica = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    numer = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresy", x => x.adres_id);
                    table.ForeignKey(
                        name: "FK_Adresy_Użytkownicy_użytkownik_id",
                        column: x => x.użytkownik_id,
                        principalTable: "Użytkownicy",
                        principalColumn: "użytkownik_id");
                });

            migrationBuilder.CreateTable(
                name: "Kandydaci",
                columns: table => new
                {
                    kandydat_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    użytkownik_id = table.Column<int>(type: "int", nullable: true),
                    kierunek_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ścieżka_zdjęcia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    data_aplikacji = table.Column<DateTime>(type: "date", nullable: false),
                    data_aktualizacji_statusu = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kandydaci", x => x.kandydat_id);
                    table.ForeignKey(
                        name: "FK_Kandydaci_Kierunki_kierunek_id",
                        column: x => x.kierunek_id,
                        principalTable: "Kierunki",
                        principalColumn: "kierunek_id");
                    table.ForeignKey(
                        name: "FK_Kandydaci_Użytkownicy_użytkownik_id",
                        column: x => x.użytkownik_id,
                        principalTable: "Użytkownicy",
                        principalColumn: "użytkownik_id");
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    pracownik_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    użytkownik_id = table.Column<int>(type: "int", nullable: true),
                    stanowisko = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    departament = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    obowiązki = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.pracownik_id);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Użytkownicy_użytkownik_id",
                        column: x => x.użytkownik_id,
                        principalTable: "Użytkownicy",
                        principalColumn: "użytkownik_id");
                });

            migrationBuilder.CreateTable(
                name: "Egzaminy",
                columns: table => new
                {
                    egzamin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kandydat_id = table.Column<int>(type: "int", nullable: true),
                    nazwa_egzaminu = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    wynik = table.Column<int>(type: "int", nullable: false),
                    data_przystąpienia = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egzaminy", x => x.egzamin_id);
                    table.ForeignKey(
                        name: "FK_Egzaminy_Kandydaci_kandydat_id",
                        column: x => x.kandydat_id,
                        principalTable: "Kandydaci",
                        principalColumn: "kandydat_id");
                });

            migrationBuilder.CreateTable(
                name: "Płatności",
                columns: table => new
                {
                    płatność_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kandydat_id = table.Column<int>(type: "int", nullable: true),
                    data_płatności = table.Column<DateTime>(type: "date", nullable: false),
                    kwota = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    metoda = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Płatności", x => x.płatność_id);
                    table.ForeignKey(
                        name: "FK_Płatności_Kandydaci_kandydat_id",
                        column: x => x.kandydat_id,
                        principalTable: "Kandydaci",
                        principalColumn: "kandydat_id");
                });

            migrationBuilder.CreateTable(
                name: "Dokumenty",
                columns: table => new
                {
                    dokument_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KandydatId = table.Column<int>(type: "int", nullable: true),
                    PracownicyId = table.Column<int>(type: "int", nullable: true),
                    Rodzaj = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Uwagi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Komentarz = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ŚcieżkaDokumentu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataPrzesłania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAktualizacjiStatusu = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumenty", x => x.dokument_id);
                    table.ForeignKey(
                        name: "FK_Dokumenty_Kandydaci_KandydatId",
                        column: x => x.KandydatId,
                        principalTable: "Kandydaci",
                        principalColumn: "kandydat_id");
                    table.ForeignKey(
                        name: "FK_Dokumenty_Pracownicy_PracownicyId",
                        column: x => x.PracownicyId,
                        principalTable: "Pracownicy",
                        principalColumn: "pracownik_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresy_użytkownik_id",
                table: "Adresy",
                column: "użytkownik_id");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenty_KandydatId",
                table: "Dokumenty",
                column: "KandydatId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenty_PracownicyId",
                table: "Dokumenty",
                column: "PracownicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Egzaminy_kandydat_id",
                table: "Egzaminy",
                column: "kandydat_id");

            migrationBuilder.CreateIndex(
                name: "IX_Kandydaci_kierunek_id",
                table: "Kandydaci",
                column: "kierunek_id",
                unique: true,
                filter: "[kierunek_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Kandydaci_użytkownik_id",
                table: "Kandydaci",
                column: "użytkownik_id",
                unique: true,
                filter: "[użytkownik_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Płatności_kandydat_id",
                table: "Płatności",
                column: "kandydat_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_użytkownik_id",
                table: "Pracownicy",
                column: "użytkownik_id",
                unique: true,
                filter: "[użytkownik_id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresy");

            migrationBuilder.DropTable(
                name: "Dokumenty");

            migrationBuilder.DropTable(
                name: "Egzaminy");

            migrationBuilder.DropTable(
                name: "Płatności");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Kandydaci");

            migrationBuilder.DropTable(
                name: "Kierunki");

            migrationBuilder.DropTable(
                name: "Użytkownicy");
        }
    }
}
