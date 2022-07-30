using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Items.Microservice
{
    public partial class workshopitemsdbContext : DbContext
    {
        public workshopitemsdbContext()
        {
        }

        public workshopitemsdbContext(DbContextOptions<workshopitemsdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemCollection> ItemCollections { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ItemCollectionId).HasColumnName("item_collection_id");

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(150)
                    .HasColumnName("item_description");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(30)
                    .HasColumnName("item_name");

                entity.Property(e => e.ItemOwnerId).HasColumnName("item_owner_id");

                entity.Property(e => e.ItemPrice)
                    .HasColumnType("money")
                    .HasColumnName("item_price");

                entity.Property(e => e.ItemType)
                    .HasMaxLength(30)
                    .HasColumnName("item_type");

                entity.HasOne(d => d.ItemCollection)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemCollectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Item_item_collection_id_fkey");
            });

            modelBuilder.Entity<ItemCollection>(entity =>
            {
                entity.HasKey(e => e.CollectionId)
                    .HasName("ItemCollection_pkey");

                entity.ToTable("ItemCollection");

                entity.Property(e => e.CollectionId)
                    .HasColumnName("collection_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CollectionDescription)
                    .HasMaxLength(150)
                    .HasColumnName("collection_description");

                entity.Property(e => e.CollectionName)
                    .HasMaxLength(30)
                    .HasColumnName("collection_name");

                entity.Property(e => e.CollectionOwnerId).HasColumnName("collection_owner_id");

                entity.Property(e => e.CollectionPrice)
                    .HasColumnType("money")
                    .HasColumnName("collection_price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
