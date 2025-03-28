using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgettoSettimana7.Models.Auth;
using ProgettoSettimana7.Models;

namespace ProgettoSettimana7.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ApplicationUserRole>()
                .HasOne(a => a.ApplicationUser)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(a => a.UserId);

            modelBuilder
                .Entity<ApplicationUserRole>()
                .HasOne(a => a.ApplicationRole)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(a => a.RoleId);

            modelBuilder
                .Entity<Ticket>()
                .HasOne(t => t.ApplicationUser)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId);

            modelBuilder
                .Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EventId);

            modelBuilder
                .Entity<Event>()
                .HasOne(e => e.Artist)
                .WithMany(a => a.Events)
                .HasForeignKey(e => e.ArtistId);

            // Tab ApplicationRoles
            modelBuilder
                .Entity<ApplicationRole>()
                .HasData(
                    new ApplicationRole()
                    {
                        Id = "feeaad97-2197-4d4f-9c59-2b5cdbe6aafb",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                    },
                    new ApplicationRole()
                    {
                        Id = "4a3ecee1-bb71-4931-a484-6597abe37190",
                        Name = "User",
                        NormalizedName = "User",
                    }
                );

            // Tab ApplicationUser
            modelBuilder
                .Entity<ApplicationUser>()
                .HasData(
                    // pw = ciaociao
                    new ApplicationUser()
                    {
                        Id = "e2af1de3-d002-4e20-bdb2-63188bbea7d9",
                        FirstName = "Admin",
                        LastName = "Admin",
                        Email = "admin@mail.com",
                        UserName = "admin@mail.com",
                        NormalizedUserName = "ADMIN@MAIL.COM",
                        NormalizedEmail = "ADMIN@MAIL.COM",
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEExcq7x3RhWBDQjtOrJT3QDKUMxGdWfHt8yspkXASF7Z10PFvcGGV2BR3+ekJcMnkg==",
                        PhoneNumber = "1231231230",
                    },

                    // pw = tommasomancini
                    new ApplicationUser()
                    {
                        Id = "c78b6d05-e718-4564-b3d2-f6905c6a469b",
                        FirstName = "Tommaso",
                        LastName = "Mancini",
                        Email = "tommaso.mancini@mail.com",
                        UserName = "tommaso.mancini@mail.com",
                        NormalizedUserName = "TOMMASO.MANCINI@MAIL.COM",
                        NormalizedEmail = "TOMMASO.MANCINI@MAIL.COM",
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEP4/6WxQ3M9gftzXujnPJK3V+5e6HUclFcmr35MahbA3rRR2k+Qe2XINMYhRWOhD6A==",
                        PhoneNumber = "1234567890",
                    },
                    // pw = sandradiberto
                    new ApplicationUser()
                    {
                        Id = "3c209b02-7b38-42c1-a05e-28570930b7bc",
                        FirstName = "Sandra",
                        LastName = "Di Berto",
                        Email = "sandra.diberto@mail.com",
                        UserName = "sandra.diberto@mail.com",
                        NormalizedUserName = "SANDRA.DIBERTO@MAIL.COM",
                        NormalizedEmail = "SANDRA.DIBERTO@MAIL.COM",
                        PasswordHash =
                            "AQAAAAIAAYagAAAAEO+lm/3EWXuT6zSmVkOuXFvMIcAn/LRckKCPlHZKN22Alm/OTWTpk6oH1F3Jh7P4yQ==",
                        PhoneNumber = "9876543210",
                    }
                );

            // Tab ApplicationUserRoles
            modelBuilder
                .Entity<ApplicationUserRole>()
                .HasData(
                    new ApplicationUserRole()
                    {
                        UserId = "e2af1de3-d002-4e20-bdb2-63188bbea7d9",
                        RoleId = "feeaad97-2197-4d4f-9c59-2b5cdbe6aafb",
                    },
                    new ApplicationUserRole()
                    {
                        UserId = "c78b6d05-e718-4564-b3d2-f6905c6a469b",
                        RoleId = "4a3ecee1-bb71-4931-a484-6597abe37190",
                    },
                    new ApplicationUserRole()
                    {
                        UserId = "3c209b02-7b38-42c1-a05e-28570930b7bc",
                        RoleId = "4a3ecee1-bb71-4931-a484-6597abe37190",
                    }
                );
        }
    }

}
