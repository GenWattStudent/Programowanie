using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Rekrutacja.Models;

[Table("Pracownicy")]
public partial class Pracownicy
{
    [Key]
    [Column("pracownik_id")]
    public int PracownikId { get; set; }

    [Column("użytkownik_id")]
    public int UżytkownikId { get; set; }

    [Column("stanowisko")]
    [StringLength(20)]
    public string Stanowisko { get; set; } = null!;

    [Column("departament")]
    [StringLength(20)]
    public string Departament { get; set; } = null!;

    [Column("obowiązki")]
    [StringLength(30)]
    public string Obowiązki { get; set; } = null!;

    public virtual Użytkownicy Użytkownik { get; set; }
    public virtual ICollection<Dokumenty>? DokumentyPracowników { get; set; }
}
