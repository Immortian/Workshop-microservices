using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Users.Microservice
{
    public partial class workshopusersdbContext : DbContext
    {
        public workshopusersdbContext()
        {
        }

        public workshopusersdbContext(DbContextOptions<workshopusersdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.UserEmail, "User_user_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "User_user_name_key")
                    .IsUnique();

                entity.Property(e => e.WalletBalance)
                    .HasColumnName("wallet_balance");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(30)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(30)
                    .HasColumnName("user_password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
