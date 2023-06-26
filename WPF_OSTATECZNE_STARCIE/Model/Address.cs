namespace WPF_OSTATECZNE_STARCIE.Model;

public partial class Address {
    public int AddressID { get; set; }
    public int EmployeeID { get; set; }
    public Employee Employee { get; set; }
    public string Addres { get; set; }
    public string Type { get; set; }
}