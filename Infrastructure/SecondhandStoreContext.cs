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
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportImage> ReportImages { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
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

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.CredibilityPoint).HasColumnName("credibilityPoint");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

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
                    .HasConstraintName("FK__Account__roleId__236943A5");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");

                entity.Property(e => e.CategoryValue).HasColumnName("categoryValue");
            });

            modelBuilder.Entity<ExchangeOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Exchange__0809335DF24ACEBF");

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
                    .HasConstraintName("FK__ExchangeO__buyer__37703C52");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.ExchangeOrders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__order__3864608B");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.ExchangeOrders)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__postI__3587F3E0");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.ExchangeOrderSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExchangeO__selle__367C1819");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(4000)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Image__postId__2EDAF651");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.IsDonated).HasColumnName("isDonated");

                entity.Property(e => e.PostStatusId).HasColumnName("postStatusId");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .HasColumnName("productName");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__accountId__2A164134");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__categoryId__2B0A656D");

                entity.HasOne(d => d.PostStatus)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__postStatus__2BFE89A6");
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

                entity.Property(e => e.ReportStatusId).HasColumnName("reportStatusId");

                entity.Property(e => e.ReportedAccountId).HasColumnName("reportedAccountId");

                entity.Property(e => e.ReporterId).HasColumnName("reporterId");

                entity.HasOne(d => d.ReportStatus)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ReportStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reportSt__3D2915A8");

                entity.HasOne(d => d.ReportedAccount)
                    .WithMany(p => p.ReportReportedAccounts)
                    .HasForeignKey(d => d.ReportedAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reported__3C34F16F");

                entity.HasOne(d => d.Reporter)
                    .WithMany(p => p.ReportReporters)
                    .HasForeignKey(d => d.ReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__3B40CD36");
            });

            modelBuilder.Entity<ReportImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__ReportIm__336E9B55101008E1");

                entity.ToTable("ReportImage");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(4000)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportImages)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReportIma__repor__625A9A57");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("reviewId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .HasColumnName("description");

                entity.Property(e => e.RatingStar).HasColumnName("ratingStar");

                entity.Property(e => e.ReviewedId).HasColumnName("reviewedId");

                entity.Property(e => e.ReviewerId).HasColumnName("reviewerId");

                entity.HasOne(d => d.Reviewed)
                    .WithMany(p => p.ReviewRevieweds)
                    .HasForeignKey(d => d.ReviewedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__reviewed__503BEA1C");

                entity.HasOne(d => d.Reviewer)
                    .WithMany(p => p.ReviewReviewers)
                    .HasForeignKey(d => d.ReviewerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__reviewer__4F47C5E3");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(2)
                    .HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(255)
                    .HasColumnName("statusName");
            });

            modelBuilder.Entity<TopUp>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__TopUp__0809335D9785E6D5");

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
                    .HasConstraintName("FK__TopUp__accountId__31B762FC");

                entity.HasOne(d => d.TopupStatus)
                    .WithMany(p => p.TopUps)
                    .HasForeignKey(d => d.TopupStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TopUp__topupStat__32AB8735");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
