namespace GradFolio.Core.DTO
{
    public class PortalOverviewDto
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
      


        public ProfileDto Profile { get; set; }
        public ExperienceDto LatestExperience { get; set; }
        public CourseDto LatestCourse { get; set; }
        public SkillDto LatestSkill { get; set; }
        public AwardDto LatestAward { get; set; }
        public InterestDto LatestInterest { get; set; }
        public ProjectDto LatestProject { get; set; }
        public CurriculumVitaeDto LatestCv { get; set; }
        public PortfolioDto LatestPortfolio { get; set; }

        //public int CvCount { get; set; }
        //portfolio
    }

}
