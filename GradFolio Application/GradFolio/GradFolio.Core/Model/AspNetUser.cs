using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradFolio.Core.Model
{
    public class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            Awards = new HashSet<Award>();
            Courses = new HashSet<Course>();
            CurriculumVitaes = new HashSet<CurriculumVitae>();
            Experiences = new HashSet<Experience>();
            Interests = new HashSet<Interest>();
            Portfolios = new HashSet<Portfolio>();
            Profiles = new HashSet<Profile>();
            Projects = new HashSet<Project>();
            Skills = new HashSet<Skill>();
            AspNetRoles = new HashSet<AspNetRole>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }

        public virtual ICollection<Award> Awards { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<CurriculumVitae> CurriculumVitaes { get; set; }

        public virtual ICollection<Experience> Experiences { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }

        public virtual ICollection<Portfolio> Portfolios { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
