using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Model.Identity;

namespace NTEcommerce.WebAPI.DBContext
{
    public class EcommerceDbContext : IdentityDbContext<User, Role, Guid>
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }

        protected EcommerceDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> Images { get; set; }
        public DbSet<ProductReview> Reviews { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            var userEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is User && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            foreach (var entityEntry in userEntries)
            {
                ((User)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((User)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            var userEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is User && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            var productEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Product && (
                    e.State == EntityState.Added
                ));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            foreach (var entityEntry in userEntries)
            {
                ((User)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((User)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            // foreach (var entityEntry in productEntries)
            // {
            //     var product = ((Product)entityEntry.Entity);
            //     if(product.Category != null){
            //         product.Category.TotalProducts++;
            //         if(product.Category.ParentCategory != null){
            //             product.Category.ParentCategory.TotalProducts++;
            //         }
            //     }
            // }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            User admin = new()
            {
                Id = Guid.Parse("8f9c2357-5b1c-4ea9-9b86-a9f93ac5efa8"),
                UserName = "Admin",
                NormalizedUserName = "Admin",
                FullName = "Hương Khôn Vũ",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            PasswordHasher<User> passwordHasher = new();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123");

            Role adminRole = new()
            {
                Id = Guid.Parse("296722a7-b5ca-4bc7-8bfb-b3f507f6613f"),
                Name = RoleConstant.Admin,
                NormalizedName = RoleConstant.Admin
            };

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
                entity.HasData(admin);
            });
            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Role");
                entity.HasData(adminRole);
            });
            builder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
                entity.HasData(new IdentityUserRole<Guid>() { UserId = admin.Id, RoleId = adminRole.Id });
            });
            builder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });
            builder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
            builder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
            });
            builder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable(name: "UserTokens");
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasOne(x => x.ParentCategory)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.ParentCategoryId);

                entity.HasIndex(x => x.Name)
                .IsUnique();
            });
            base.OnModelCreating(builder);
        }
    }
}
