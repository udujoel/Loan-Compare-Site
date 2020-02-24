namespace LoanCompareSite.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoanComparerModel : DbContext
    {
        public LoanComparerModel()
            : base("name=LoanComparerCFDBModel")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<loandetail> loandetails { get; set; }
        public virtual DbSet<request> requests { get; set; }
        public virtual DbSet<subscription> subscriptions { get; set; }
        public virtual DbSet<visitcount> visitcounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<loandetail>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<loandetail>()
                .Property(e => e.package)
                .IsUnicode(false);

            modelBuilder.Entity<loandetail>()
                .Property(e => e.terms)
                .IsUnicode(false);

            modelBuilder.Entity<loandetail>()
                .Property(e => e.website)
                .IsUnicode(false);

            modelBuilder.Entity<request>()
                .HasOptional(e => e.request1)
                .WithRequired(e => e.request2);

            modelBuilder.Entity<visitcount>()
                .HasOptional(e => e.visitcount1)
                .WithRequired(e => e.visitcount2);
        }
    }
}
