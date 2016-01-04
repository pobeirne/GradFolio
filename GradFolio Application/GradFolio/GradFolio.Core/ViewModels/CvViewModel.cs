using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GradFolio.Core.ViewModels
{
    public class CvOverviewViewModel
    {
        public bool IsReady { get; set; }
        public PortalOverviewStats Stats { get; set; }
        public IEnumerable<CvFormViewModel> CvList { get; set; }
    }

    public class CvTemplateType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class CvViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Type { get; set; }

        public long RefNum { get; set; }

        [Required]
        [StringLength(128)]
        public string Experience1 { get; set; }

        [Required]
        [StringLength(128)]
        public string Experience2 { get; set; }


        [Required]
        [StringLength(128)]
        public string Course1 { get; set; }


        [StringLength(128)]
        public string Course4 { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public class CvExperience
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }
    }

    public class CvCourse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string College { get; set; }
    }

    //public class CvViewModel
    //{
    //    public bool Status { get; set; }
    //    public ProfileViewModel Profile { get; set; }
    //    public List<CvModel> CvList { get; set; }
    //    public List<ExperienceViewModel> Experiences { get; set; }
    //    public List<CourseViewModel> Courses { get; set; }
    //    public List<SkillViewModel> Skills { get; set; }
    //    public List<AwardViewModel> Awards { get; set; }
    //    public List<InterestViewModel> Interests { get; set; }
    //    public List<ProjectViewModel> Projects { get; set; }
    //}

    public class CvFormViewModel
    {
        public Guid Id { get; set; }
        
        [Display(Name = "CV Name:")]
        [Required(ErrorMessage = "Please provide a skill Title.")]
        [StringLength(100)]
        public string FileName { get; set; }

        [StringLength(150)]
        public string Type { get; set; }
       
        public long RefNum { get; set; }

        [Display(Name = "Experience 1:")]
        [Required(ErrorMessage = "Please provide a skill Title.")]
        public string SelectedExp1 { get; set; }

        [Display(Name = "Experience 2:")]
        [Required(ErrorMessage = "Please provide a skill Title.")]
        public string SelectedExp2 { get; set; }


        [Display(Name = "Course 1:")]
        [Required(ErrorMessage = "Please provide a skill Title.")]
        public string SelectedCourse1 { get; set; }

        [Display(Name = "Course 2:")]
        [Required(ErrorMessage = "Please provide a skill Title.")]
        public string SelectedCourse2 { get; set; }

        [Display(Name = "Start Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        public IEnumerable<SelectListItem> Experiences { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
