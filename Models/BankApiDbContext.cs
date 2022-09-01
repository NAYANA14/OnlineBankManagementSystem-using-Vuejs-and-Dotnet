using Microsoft.EntityFrameworkCore;
namespace BankManagementDotnetApi.Models
{
    public class BankApiDbContext: DbContext
    { 
        public BankApiDbContext(DbContextOptions<BankApiDbContext> options) : base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }= null!;
        public DbSet<Customer> Customers { get; set; }= null!;
        public DbSet<Account> Accounts { get; set; }= null!;
        public DbSet<Transaction> Transactions { get; set; }= null!;
        public DbSet<RegisteredPayee> RegisteredPayees { get; set; }= null!;
         public DbSet<MoneyTransfer> MoneyTransfers { get; set; }= null!;
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }
        
    }
}