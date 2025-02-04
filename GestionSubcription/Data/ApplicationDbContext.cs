using GestionSubcription.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace GestionSubcription.Data

{
    //public class ApplicationDbContext:DbContext
    //{
    //    public ApplicationDbContext(
    //        DbContextOptions<ApplicationDbContext> options ) : base( options) { }
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}
 
        public DbSet<Client> Clients { get; set; }
        public DbSet<Application> Applications { get; set; }  
        public DbSet<Subscription> Subscriptions { get; set; }
    }





}
