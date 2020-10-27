using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebsitePerfomance.Models;
using WebsitePerfomanceManager.Business.Interfaces;
using WebsitePerfomanceManager.Business.Models;

namespace WebsitePerfomance.Controllers
{
    public class WebsitePerfomanceController : Controller
    {
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public WebsitePerfomanceController(ISiteService siteService, IMapper mapper)
        {
            _siteService = siteService;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Measure(SitePostModel site)
        {
            if(site.Url!=null)
            {
                var siteModel = _mapper.Map<SiteModel>(site);
                string sitemap = _siteService.GetSitemap(site.Url);
                var measure = _siteService.MeasureSpeedResponse(sitemap);

                if(measure==null)
                {
                    return View("SiteMapNotFound");
                }

                else
                {
                    siteModel.Measurements.Add(measure);

                    _siteService.Add(siteModel, measure);

                    var currentSite = _siteService.GetMinMaxValues(siteModel);

                    var result = _mapper.Map<SiteViewModel>(currentSite);

                    result.Measurements = result.Measurements.OrderBy(m => m.ResponseTime).ToList();
                    result.Sites = GetSelectList();

                    return View(result);
                }                
            }

            return View("Index");
        }

        public ActionResult TablePartial()
        {
            return PartialView();
        }

        public ActionResult ChartPartial()
        {
            return PartialView();
        }

        public ActionResult ChartArrayBasic(int id)
        {
            var siteModel = _siteService.GetSiteById(id);
            var chart = _siteService.GetChartStatistics(siteModel);

            var chartStatisticsModel = new ChartStatisticsViewModel();
            chartStatisticsModel.Chart = chart;
            chartStatisticsModel.Chart.Write();

            return null;
        }

        public ActionResult Details(int SelectedId)
        {
            var site = _siteService.GetSiteById(SelectedId);
            var siteViewModel = _mapper.Map<SiteViewModel>(site);
            siteViewModel.Measurements = siteViewModel.Measurements.OrderBy(m => m.ResponseTime).ToList();

            return View(siteViewModel);
        }

        private IEnumerable<SelectListItem> GetSelectList()
        {
            var sites = _siteService.GetAll();
            var sitesViewModel = _mapper.Map<IList<SiteViewModel>>(sites);

            var selectList = from item in sitesViewModel
                          select new SelectListItem { Text = item.Url, Value = item.Id.ToString() };

            return selectList;
        }

        public ActionResult SiteMapNotFound()
        {
            return View();
        }
    }
}