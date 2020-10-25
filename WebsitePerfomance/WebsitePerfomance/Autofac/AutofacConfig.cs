using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using WebsitePerfomance.Data.Interfaces;
using WebsitePerfomance.Data.Repositories;
using WebsitePerfomanceManager.Business.Interfaces;
using WebsitePerfomanceManager.Business.Services;

namespace WebsitePerfomance.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<SiteRepository>().As<ISiteRepository>();
            builder.RegisterType<SiteService>().As<ISiteService>();

            builder.RegisterModule<MapperAutofacModul>(); 

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}