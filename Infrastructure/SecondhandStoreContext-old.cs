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
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TopUp> TopUps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .HasColumnName("accountId");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

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

                entity.Property(e => e.UserRatingScore).HasColumnName("userRatingScore");

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
                    .HasName("PK__Exchange__0809335DF3C52DBC");

                entity.ToTable("ExchangeOrder");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.BuyerId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("buyerId");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("orderStatus");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.SellerId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("sellerId");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.ExchangeOrderBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__buyer__36B12243");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.ExchangeOrders)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__postI__34C8D9D1");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.ExchangeOrderSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__selle__35BCFE0A");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.PermissionId).HasColumnName("permissionId");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(1)
                    .HasColumnName("permissionName");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(2)
                    .HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Permissio__roleI__2F10007B");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .HasColumnName("accountId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasColumnName("image");

                entity.Property(e => e.PointCost).HasColumnName("pointCost");

                entity.Property(e => e.PostDate)
                    .HasColumnType("date")
                    .HasColumnName("postDate");

                entity.Property(e => e.PostExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("postExpiryDate");

                entity.Property(e => e.PostPriority).HasColumnName("postPriority");

                entity.Property(e => e.PostStatus)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("postStatus");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .HasColumnName("productName");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__accountId__2B3F6F97");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__categoryId__2C3393D0");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.Property(e => e.Evidence1)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("evidence1");

                entity.Property(e => e.Evidence2)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("evidence2");

                entity.Property(e => e.Evidence3)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("evidence3");

                entity.Property(e => e.Reason)
                    .HasMaxLength(1)
                    .HasColumnName("reason");

                entity.Property(e => e.ReportDate)
                    .HasColumnType("date")
                    .HasColumnName("reportDate");

                entity.Property(e => e.ReportedAccountId)
                    .HasMaxLength(10)
                    .HasColumnName("reportedAccountId");

                entity.Property(e => e.ReporterId)
                    .HasMaxLength(10)
                    .HasColumnName("reporterId");

                entity.HasOne(d => d.ReportedAccount)
                    .WithMany(p => p.ReportReportedAccounts)
                    .HasForeignKey(d => d.ReportedAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reported__3E52440B");

                entity.HasOne(d => d.Reporter)
                    .WithMany(p => p.ReportReporters)
                    .HasForeignKey(d => d.ReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__3D5E1FD2");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("reviewId");

                entity.Property(e => e.Content)
                    .HasMaxLength(1)
                    .HasColumnName("content");

                entity.Property(e => e.FeedbackUserId)
                    .HasMaxLength(10)
                    .HasColumnName("feedbackUserId");

                entity.Property(e => e.FeedbackUsername)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("feedbackUsername");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.StarRating).HasColumnName("starRating");

                entity.HasOne(d => d.FeedbackUser)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.FeedbackUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__feedback__3A81B327");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__postId__398D8EEE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(2)
                    .HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<TopUp>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__TopUp__0809335D351A579D");

                entity.ToTable("TopUp");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .HasColumnName("accountId");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.TopUpDate)
                    .HasColumnType("date")
                    .HasColumnName("topUpDate");

                entity.Property(e => e.TopUpPoint).HasColumnName("topUpPoint");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TopUps)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TopUp__accountId__31EC6D26");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
