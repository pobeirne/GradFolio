using GradFolio.Core.Repository;
using GradFolio.Infrastructure.Repository;
using Ninject.Modules;

namespace GradFolio.Infrastructure.DependencyInjection
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            // TO DO BINDINGS..
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IProfileRepository>().To<ProfileRepository>();
            Bind<IExperienceRepository>().To<ExperienceRepository>();
            Bind<ICourseRepository>().To<CourseRepository>();
            Bind<ISkillRepository>().To<SkillRepository>();
            Bind<IAwardRepository>().To<AwardRepository>();
            Bind<IInterestRepository>().To<InterestRepository>();
            Bind<IProjectRepository>().To<ProjectRepository>();

            Bind<ICurriculumVitaeRepository>().To<CurriculumVitaeRepository>();
            Bind<IPortfolioRepository>().To<PorfolioRepository>();
        }
    }
}
