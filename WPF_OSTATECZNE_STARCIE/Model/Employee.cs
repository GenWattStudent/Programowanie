using System.Collections.Generic;

namespace WPF_OSTATECZNE_STARCIE.Model;

public partial class Employee {
    public int EmployeeID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get { return FirstName + " " + LastName; } }
    public string Email { get; set; }
    public string Phone { get; set; }
    public System.DateTime HireDate { get; set; }
    public int ManagerID { get; set; }
    public float Salary { get; set; }
    public int DepartmentID { get; set; }
    public ICollection<Address> Addresses { get; set; }
}