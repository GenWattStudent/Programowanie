using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rekrutacja.Models;

[Table("Dokumenty")]
public partial class Dokumenty
{
    [Key]
    [Column("dokument_id")]
    public int DokumentId { get; set; }

    public int KandydatId { get; set; }

    public int PracownicyId { get; set; }

    [StringLength(20)]
    public string Rodzaj { get; set; } = null!;

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [StringLength(250)]
    public string? Uwagi { get; set; }

    [StringLength(250)]
    public string? Komentarz { get; set; }

    [StringLength(100)]
    public string ŚcieżkaDokumentu { get; set; } = null!;

    public DateTime DataPrzesłania { get; set; }

    public DateTime DataAktualizacjiStatusu { get; set; }

    public virtual Pracownicy Pracownicy { get; set; }
    public virtual Kandydaci Kandydaci { get; set; }
}
