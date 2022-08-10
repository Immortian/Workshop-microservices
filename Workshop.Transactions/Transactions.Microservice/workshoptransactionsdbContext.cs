using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Transactions.Microservice
{
    public partial class workshoptransactionsdbContext : DbContext
    {
        public workshoptransactionsdbContext()
        {
        }

        public workshoptransactionsdbContext(DbContextOptions<workshoptransactionsdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TransactionInfo> TransactionInfos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionInfo>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("Transaction_info_pkey");

                entity.ToTable("Transaction_info");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BuyerId).HasColumnName("buyer_id");

                entity.Property(e => e.CollectionId).HasColumnName("collection_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.SellerId).HasColumnName("seller_id");

                entity.Property(e => e.TransactionDatetime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("transaction_datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
