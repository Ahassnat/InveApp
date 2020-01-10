using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.API.Models;

namespace Test.API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                                UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                                IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> Options) : base(Options) { }
        public DbSet<Value> Values { get; set; }
        public DbSet<Gallary> Gallaries { get; set; }
        public DbSet<BodyDetail> BodyDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // configure the schema needed for Identity

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();
                        
                userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });

            builder.Entity<Message>()
                                .HasOne(u => u.Sender)
                                .WithMany(m => m.MessagesSent)
                                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Message>()
                                .HasOne(u => u.Recipient)
                                .WithMany(m => m.MessagesReceived)
                                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}