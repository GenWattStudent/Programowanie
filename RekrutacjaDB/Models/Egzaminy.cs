using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rekrutacja.Models;

[Table("Egzaminy")]
public partial class Egzaminy
{
    [Key]
    [Column("egzamin_id")]
    public int EgzaminId { get; set; }

    [Column("kandydat_id")]
    public int KandydatId { get; set; }

    [Column("nazwa_egzaminu")]
    [StringLength(40)]
    public string NazwaEgzaminu { get; set; } = null!;

    [Column("wynik")]
    public int Wynik { get; set; }

    [Column("data_przystąpienia", TypeName = "date")]
    public DateTime DataPrzystąpienia { get; set; }

    public virtual Kandydaci Kandydat { get; set; }
}
