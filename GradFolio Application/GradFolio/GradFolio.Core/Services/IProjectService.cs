using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Services
{
    public interface IProjectService
    {
        ProjectDto GetProjectById(string userId, string projectId);
        IEnumerable<ProjectDto> GetProjectList(string userId);
        bool InsertProject(string userId, ProjectDto project);
        bool UpdateProject(string userId, ProjectDto project);
        bool DeleteProject(string userId, string projectId);
    }
}
