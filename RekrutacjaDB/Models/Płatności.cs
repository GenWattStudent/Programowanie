using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rekrutacja.Models;

[Table("Płatności")]
public partial class Płatności
{
    [Key]
    [Column("płatność_id")]
    public int PłatnośćId { get; set; }

    [Column("kandydat_id")]
    public int KandydatId { get; set; }

    [Column("data_płatności", TypeName = "date")]
    public DateTime DataPłatności { get; set; }

    [Column("kwota")]
    public double Kwota { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column("metoda")]
    [StringLength(20)]
    public string Metoda { get; set; } = null!;

    public virtual Kandydaci Kandydat { get; set; }
}
