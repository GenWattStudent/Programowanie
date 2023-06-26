using Microsoft.EntityFrameworkCore;

namespace WPF_OSTATECZNE_STARCIE.Model;

public class ApplicationContext : DbContext {

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-HA5MSFQO\SQLEXPRESS;Database=Cośtam;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;");
    }
}