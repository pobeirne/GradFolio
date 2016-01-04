using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services;
using Ninject.Modules;

namespace GradFolio.Infrastructure.DependencyInjection
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            //TO DO BINDINGS..
            Bind<IUserService>().To<UserService>();
            Bind<IPortalService>().To<PortalService>();
            Bind<IProfileService>().To<ProfileService>();
            Bind<IExperienceService>().To<ExperienceService>();
            Bind<ICourseService>().To<CourseService>();
            Bind<ISkillService>().To<SkillService>();
            Bind<IAwardService>().To<AwardService>();
            Bind<IInterestService>().To<InterestService>();
            Bind<IProjectService>().To<ProjectService>();
            
            Bind<ICurriculumVitaeService>().To<CurriculumVitaeService>();
            Bind<IPortfolioService>().To<PortfolioService>();
        }
    }
}
