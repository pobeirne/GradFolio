using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Repository
{
    public interface IProjectRepository
    {
        ProjectDto GetProjectById(string projectId);
        IEnumerable<ProjectDto> GetAllProjectByUserId(string userId);
        bool InsertProject(ProjectDto project);
        bool UpdateProject(ProjectDto project);
        bool DeleteProject(string projectId);
        bool Save();
    }
}
