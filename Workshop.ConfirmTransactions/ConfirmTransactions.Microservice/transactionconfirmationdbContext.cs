using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConfirmTransactions.Microservice
{
    public partial class transactionconfirmationdbContext : DbContext
    {
        public transactionconfirmationdbContext()
        {
        }

        public transactionconfirmationdbContext(DbContextOptions<transactionconfirmationdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TransactionConfirmation> TransactionConfirmations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionConfirmation>(entity =>
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
                entity.Property(e => e.IsWalletBalanceOk).HasColumnName("is_wallet_balance_ok");
                entity.Property(e => e.IsItemOwnerOk).HasColumnName("is_item_owner_ok");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
