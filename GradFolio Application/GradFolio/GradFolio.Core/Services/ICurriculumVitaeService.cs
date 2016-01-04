using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Services
{
    public interface ICurriculumVitaeService
    {
        CurriculumVitaeDto GetCurriculumVitaeById(string userId, string cvId);
        IEnumerable<CurriculumVitaeDto> GetAllCurriculumVitaeByUserId(string userId);
        bool InsertCurriculumVitae(string userId, CurriculumVitaeDto cv);
        bool UpdateCurriculumVitae(string userId, CurriculumVitaeDto cv);
        bool DeleteCurriculumVitae(string userId, string cvId);

        bool GenerateCurriculumVitae(string userId, string filename);

        CvTemplateDto GetCvTemplate(string cvId);
    }
}
