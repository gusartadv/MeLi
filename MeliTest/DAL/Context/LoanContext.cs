using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class LoanContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Target> Targets { get; set; }

        public DbSet<User> Users { get; set; }

        public LoanContext(DbContextOptions<LoanContext> options) : base(options) {}
    }
}
