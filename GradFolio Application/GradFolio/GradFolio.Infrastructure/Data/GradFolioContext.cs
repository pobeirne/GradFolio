using System.Data.Entity;
using GradFolio.Core.Model;

namespace GradFolio.Infrastructure.Data
{
    public class GradFolioContext : DbContext
    {
        public GradFolioContext()
            : base("name=GradFolioContext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Award> Awards { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CurriculumVitae> CurriculumVitaes { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Awards)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.CurriculumVitaes)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Experiences)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Interests)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Portfolios)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Skills)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Award>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Award>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<Award>()
                .Property(e => e.IssuedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.College)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<CurriculumVitae>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CurriculumVitae>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Experience>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Experience>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<Experience>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<Experience>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Interest>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Interest>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<Portfolio>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.Mobile)
                .IsFixedLength();

            modelBuilder.Entity<Profile>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Profile>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.PortfolioUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.LinkedInUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<Skill>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Skill>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<Skill>()
                .Property(e => e.Level)
                .IsUnicode(false);
        }
    }
}
