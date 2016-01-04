using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradFolio.Core.DTO
{
    public class GradFolioDto
    {
        public ProfileDto Profile { get; set; }

        public CurriculumVitaeDto CurriculumVitae { get; set; }

        public IEnumerable<ExperienceDto> Experiences { get; set; }
        public IEnumerable<CourseDto> Courses { get; set; }

        public IEnumerable<SkillDto> Skills { get; set; }

        public IEnumerable<AwardDto> Awards { get; set; }

        public IEnumerable<InterestDto> Interests { get; set; }

        public IEnumerable<ProjectDto> Projects { get; set; }
    }
}
