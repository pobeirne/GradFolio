using System;

namespace GradFolio.Core.ViewModels
{
    public class PortalOverviewViewModel
    {
        public PortalOverviewStats PortalOverviewStats { get; set; }
        public ProfileOverview ProfileOverview { get; set; }
        public ExperienceOverview ExperienceOverview { get; set; }
        public CourseOverview CourseOverview { get; set; }
        public SkillOverview SkillOverview { get; set; }
        public AwardOverview AwardOverview { get; set; }
        public InterestOverview InterestOverview { get; set; }
        public ProjectOverview ProjectOverview { get; set; }
        public CvOverview CvOverview { get; set; }
        public PortfolioOverview PortfolioOverview { get; set; }
    }


    public class PortalOverviewStats
    {
        public int ProfileCount { get; set; }
        public int ExperienceCount { get; set; }
        public int CourseCount { get; set; }
        public int SkillCount { get; set; }
        public int AwardCount { get; set; }
        public int InterestCount { get; set; }
        public int ProjectCount { get; set; }
        public int CvCount { get; set; }
        public int PortfolioCount { get; set; }
    }

    public class ProfileOverview
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime JoinDate { get; set; }
    }

    public class ExperienceOverview
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class CourseOverview
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string College { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class SkillOverview
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class AwardOverview
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class InterestOverview
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class ProjectOverview
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class CvOverview
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
        public string CvUrl { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class PortfolioOverview
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string PortfolioUrl { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
