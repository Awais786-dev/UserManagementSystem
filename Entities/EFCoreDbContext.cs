using Microsoft.EntityFrameworkCore;
using User_Management_System.Models;

namespace User_Management_System.Entities
{
        public class EFCoreDbContext : DbContext
        {
        public EFCoreDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}
  
        public DbSet<User> Users { get; set; } 
        }
}