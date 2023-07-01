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

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ExchangeOrder> ExchangeOrders { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostStatus> PostStatuses { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TopUp> TopUps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=SecondhandStore;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phoneNo");

                entity.Property(e => e.PointBalance).HasColumnName("pointBalance");

                entity.Property(e => e.RoleId)
                    .IsRequired()
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
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<ExchangeOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Exchange__0809335DA6003788");

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
                    .HasConstraintName("FK__ExchangeO__buyer__3C69FB99");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.ExchangeOrders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__order__3D5E1FD2");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.ExchangeOrders)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__postI__3A81B327");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.ExchangeOrderSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__selle__3B75D760");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.PermissionId).HasColumnName("permissionId");

                entity.Property(e => e.PermissionName)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("permissionName");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Permissio__roleI__34C8D9D1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .IsRequired()
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

                entity.Property(e => e.PostStatusId).HasColumnName("postStatusId");

                entity.Property(e => e.PostTypeId).HasColumnName("postTypeId");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .IsRequired()
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

            modelBuilder.Entity<PostStatus>(entity =>
            {
                entity.ToTable("PostStatus");

                entity.Property(e => e.PostStatusId).HasColumnName("postStatusId");

                entity.Property(e => e.PostStatusName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("postStatusName");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.ToTable("PostType");

                entity.Property(e => e.PostTypeId).HasColumnName("postTypeId");

                entity.Property(e => e.PostTypeName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("postTypeName");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.Property(e => e.Evidence1)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("evidence1");

                entity.Property(e => e.Evidence2)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("evidence2");

                entity.Property(e => e.Evidence3)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("evidence3");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasColumnName("reason");

                entity.Property(e => e.ReportDate)
                    .HasColumnType("date")
                    .HasColumnName("reportDate");

                entity.Property(e => e.ReportedAccountId).HasColumnName("reportedAccountId");

                entity.Property(e => e.ReporterId).HasColumnName("reporterId");

                entity.HasOne(d => d.ReportedAccount)
                    .WithMany(p => p.ReportReportedAccounts)
                    .HasForeignKey(d => d.ReportedAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reported__44FF419A");

                entity.HasOne(d => d.Reporter)
                    .WithMany(p => p.ReportReporters)
                    .HasForeignKey(d => d.ReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__440B1D61");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("reviewId");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasColumnName("content");

                entity.Property(e => e.FeedbackUserId).HasColumnName("feedbackUserId");

                entity.Property(e => e.FeedbackUsername)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("feedbackUsername");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.StarRating).HasColumnName("starRating");

                entity.HasOne(d => d.FeedbackUser)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.FeedbackUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__feedback__412EB0B6");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__postId__403A8C7D");
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
                    .HasName("PK__TopUp__0809335D279E2D7B");

                entity.ToTable("TopUp");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.TopUpDate)
                    .HasColumnType("date")
                    .HasColumnName("topUpDate");

                entity.Property(e => e.TopUpPoint).HasColumnName("topUpPoint");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TopUps)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TopUp__accountId__37A5467C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
