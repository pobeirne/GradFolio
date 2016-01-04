using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradFolio.Core.ViewModels
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title :")]
        [Required(ErrorMessage = "Please provide a Title.")]
        public string Title { get; set; }
        
        [Display(Name = "Summary :")]
        [Required(ErrorMessage = "Please provide a Summary.")]
        public string Summary { get; set; }

        [Display(Name = "Date Created:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        //public IEnumerable<ProjectResource> Resources { get; set; }
    }

    public class ProjectResource
    {
        public int Id { get; set; }
        public string UrlLink { get; set; }
        public string Type { get; set; }
    }
}
