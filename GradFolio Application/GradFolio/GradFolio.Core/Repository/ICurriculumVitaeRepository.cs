using System.Collections.Generic;
using GradFolio.Core.DTO;


namespace GradFolio.Core.Repository
{
    public interface ICurriculumVitaeRepository
    {
        CurriculumVitaeDto GetCurriculumVitaeById(string cvId);
        IEnumerable<CurriculumVitaeDto> GetAllCurriculumVitaeByUserId(string userId);
        bool InsertCurriculumVitae(CurriculumVitaeDto cv);
        bool UpdateCurriculumVitae(CurriculumVitaeDto cv);
        bool DeleteCurriculumVitae(string cvId);
        bool Save();
    }
}
