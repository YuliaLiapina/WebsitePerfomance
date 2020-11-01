using Autofac;
using AutoMapper;
using WebsitePerfomance.Data.Models;
using WebsitePerfomance.Models;
using WebsitePerfomanceManager.Business.Models;

namespace WebsitePerfomance.Autofac
{
    public class MapperAutofacModul : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Site, SiteModel>();
                cfg.CreateMap<SiteModel, Site>();
                cfg.CreateMap<SiteModel, SitePostModel>();
                cfg.CreateMap<SitePostModel, SiteModel>();
                cfg.CreateMap<SiteModel, SiteViewModel>();
                cfg.CreateMap<SiteViewModel, SiteModel>();

                cfg.CreateMap<TestingPage, TestingPageModel>();
                cfg.CreateMap<TestingPageModel, TestingPage>();
                cfg.CreateMap<TestingPageModel, TestingPageViewModel>();
                cfg.CreateMap<TestingPageViewModel, TestingPageModel>();

                cfg.CreateMap<PageSpeed, PageSpeedModel>();
                cfg.CreateMap<PageSpeedModel, PageSpeed>();
                cfg.CreateMap<PageSpeedViewModel, PageSpeedModel>();
                cfg.CreateMap<PageSpeedModel, PageSpeedViewModel>();

            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
          .As<IMapper>()
          .InstancePerLifetimeScope();
        }
    }
}