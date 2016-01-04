using System.Collections.Generic;

namespace GradFolio.Core.DTO
{
    public class CvTemplateDto
    {
        public ProfileDto Profile { get; set; }

        public IEnumerable<ExperienceDto> Experiences { get; set; }
        public IEnumerable<CourseDto> Courses { get; set; }

        public IEnumerable<SkillDto> Skills { get; set; }

        public IEnumerable<AwardDto> Awards { get; set; }

        public IEnumerable<InterestDto> Interests { get; set; }

        public IEnumerable<ProjectDto> Projects { get; set; }
    }
}
