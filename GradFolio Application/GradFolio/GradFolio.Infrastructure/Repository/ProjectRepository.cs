using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using GradFolio.Core.DTO;
using GradFolio.Core.Model;
using GradFolio.Core.Repository;
using GradFolio.Infrastructure.Data;

namespace GradFolio.Infrastructure.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public ProjectDto GetProjectById(string projectId)
        {
            var project = _context.Projects.FirstOrDefault(x => x.Id.ToString() == projectId);
            if (project == null) return null;
            return new ProjectDto
            {
                Id = project.Id,
                UserId = project.UserId,
                Title = project.Title,
                Summary = project.Summary,
                CreateDate = project.CreateDate
            };
        }

        public IEnumerable<ProjectDto> GetAllProjectByUserId(string userId)
        {
            var projects = _context.Projects.Where(x => x.UserId == userId);
            if (!projects.Any()) return null;

            var resultList = new List<ProjectDto>();
            foreach (var project in projects)
            {
                resultList.Add(new ProjectDto
                {
                    Id = project.Id,
                    UserId = project.UserId,
                    Title = project.Title,
                    Summary = project.Summary,
                    CreateDate = project.CreateDate
                });
            }
            return resultList;
        }

        public bool InsertProject(ProjectDto project)
        {
            return _context.Projects.Add(new Project
            {
                UserId = project.UserId,
                Title = project.Title,
                Summary = project.Summary,
                CreateDate = DateTime.Now
            }) != null;
        }

        public bool UpdateProject(ProjectDto project)
        {
            var record = _context.Projects.FirstOrDefault(x => x.Id == project.Id);
            if (record == null) return false;

            record.Title = project.Title;
            record.Summary = project.Summary;

            _context.Projects.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteProject(string projectId)
        {
            var record = _context.Projects.FirstOrDefault(x => x.Id.ToString() == projectId);
            return _context.Projects.Remove(record) != null;
        }

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
