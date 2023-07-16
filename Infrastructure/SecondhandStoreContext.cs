using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SecondhandStore.Models;

namespace SecondhandStore.Infrastructure
{
    public partial class SecondhandStoreContext : DbContext
    {
        public SecondhandStoreContext()
        {
        }

        public SecondhandStoreContext(DbContextOptions<SecondhandStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ExchangeOrder> ExchangeOrders { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostType> PostTypes { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<TopUp> TopUps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local);Database=SecondhandStore;Trusted_Connection=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.CredibilityPoint).HasColumnName("credibilityPoint");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phoneNo");

                entity.Property(e => e.PointBalance).HasColumnName("pointBalance");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(2)
                    .HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__roleId__267ABA7A");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<ExchangeOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Exchange__0809335D9F3AA087");

                entity.ToTable("ExchangeOrder");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.BuyerId).HasColumnName("buyerId");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");

                entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.SellerId).HasColumnName("sellerId");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.ExchangeOrderBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__buyer__3A81B327");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.ExchangeOrders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__order__3B75D760");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.ExchangeOrders)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__postI__38996AB5");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.ExchangeOrderSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__selle__398D8EEE");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(4000)
                    .HasColumnName("image");

                entity.Property(e => e.PointCost).HasColumnName("pointCost");

                entity.Property(e => e.PostDate)
                    .HasColumnType("date")
                    .HasColumnName("postDate");

                entity.Property(e => e.PostPriority).HasColumnName("postPriority");

                entity.Property(e => e.PostStatusId).HasColumnName("postStatusId");

                entity.Property(e => e.PostTypeId).HasColumnName("postTypeId");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .HasColumnName("productName");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__accountId__2F10007B");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__categoryId__30F848ED");

                entity.HasOne(d => d.PostStatus)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__postStatus__31EC6D26");

                entity.HasOne(d => d.PostType)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__postTypeId__300424B4");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.ToTable("PostType");

                entity.Property(e => e.PostTypeId).HasColumnName("postTypeId");

                entity.Property(e => e.PostTypeName)
                    .HasMaxLength(255)
                    .HasColumnName("postTypeName");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.Property(e => e.Reason)
                    .HasMaxLength(4000)
                    .HasColumnName("reason");

                entity.Property(e => e.ReportDate)
                    .HasColumnType("date")
                    .HasColumnName("reportDate");

                entity.Property(e => e.ReportedAccountId).HasColumnName("reportedAccountId");

                entity.Property(e => e.ReporterId).HasColumnName("reporterId");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.HasOne(d => d.ReportedAccount)
                    .WithMany(p => p.ReportReportedAccounts)
                    .HasForeignKey(d => d.ReportedAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reported__3F466844");

                entity.HasOne(d => d.Reporter)
                    .WithMany(p => p.ReportReporters)
                    .HasForeignKey(d => d.ReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__3E52440B");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(2)
                    .HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(6)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusName).HasMaxLength(255);
            });

            modelBuilder.Entity<TopUp>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__TopUp__0809335D20816B69");

                entity.ToTable("TopUp");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.TopUpDate)
                    .HasColumnType("date")
                    .HasColumnName("topUpDate");

                entity.Property(e => e.TopUpPoint).HasColumnName("topUpPoint");

                entity.Property(e => e.TopupStatusId).HasColumnName("topupStatusId");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TopUps)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TopUp__accountId__34C8D9D1");

                entity.HasOne(d => d.TopupStatus)
                    .WithMany(p => p.TopUps)
                    .HasForeignKey(d => d.TopupStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TopUp__topupStat__35BCFE0A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
