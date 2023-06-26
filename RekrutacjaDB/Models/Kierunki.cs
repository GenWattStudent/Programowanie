using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rekrutacja.Models;

[Table("Kierunki")]
public partial class Kierunki
{
    [Key]
    [Column("kierunek_id")]
    public int KierunekId { get; set; }

    [Column("nazwa")]
    [StringLength(20)]
    [Required(ErrorMessage = "Nazwa kierunku jest wymagana")]
    public string Nazwa { get; set; }

    [Column("opis")]
    [StringLength(500)]
    public string Opis { get; set; }

    [Column("stopień_studiów")]
    public int StopieńStudiów { get; set; }

    [Column("tryb_studiów")]
    [StringLength(14)]
    public string TrybStudiów { get; set; }

    [Column("liczba_miejsc")]
    public int LiczbaMiejsc { get; set; }
    public virtual Kandydaci? Kandydaci { get; set; }
}
