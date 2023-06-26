using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rekrutacja.Models;

public enum Płeć {
    Mężczyzna,
    Kobieta
}

[Table("Użytkownicy")]
public partial class Użytkownicy
{
    [Key]
    [Column("użytkownik_id")]
    public int UżytkownikId { get; set; }

    [Column("imię")]
    [StringLength(50, ErrorMessage = "Imię nie może być dłuższe niż 50 znaków")]  
    public string Imię { get; set; } 

    [Column("nazwisko")]
    [StringLength(50)]
    public string Nazwisko { get; set; } 

    [Column("PESEL")]
    [StringLength(11)]
    public string Pesel { get; set; } 

    [Column("telefon")]
    [StringLength(20)]
    public string Telefon { get; set; }

    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; }
    
    [Column("nr_dowodu")]
    [StringLength(20)]
    public string NrDowodu { get; set; }
    
    [Column("płeć")]
    [StringLength(10)]
    public string Płeć { get; set; }

    [Column("data_urodzenia", TypeName = "date")]
    public DateTime DataUrodzenia { get; set; }

    public virtual List<Adresy> Adresy { get; set; }

    public virtual Kandydaci? Kandydaci { get; set; }

    public virtual Pracownicy? Pracownicy { get; set; }
}