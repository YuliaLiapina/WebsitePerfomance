using AutoMapper;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;
using WebsitePerfomance.Data.Interfaces;
using WebsitePerfomance.Data.Models;
using WebsitePerfomanceManager.Business.Interfaces;
using WebsitePerfomanceManager.Business.Models;

namespace WebsitePerfomanceManager.Business.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;

        public SiteService(ISiteRepository siteRepository, IMapper mapper)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
        }
        public void Add(SiteModel siteModel)
        {
            var site = _mapper.Map<Site>(siteModel);
            _siteRepository.Add(site);
        }

        public IList<SiteModel> GetAll()
        {
            var sites = _siteRepository.GetAllSites();

            var siteModels = _mapper.Map<IList<SiteModel>>(sites);

            return siteModels;
        }

        public string GetSitemap(string url)
        {
            string siteMap;

            if (url.StartsWith("https://"))
            {
                url = url.Replace("https://", string.Empty);

                siteMap = "https://" + url.Substring(0, url.IndexOf('/')) + "/sitemap.xml";
            }
            else
            {
                siteMap = "https://" + url + "/sitemap.xml";
            }

            return siteMap;
        }

        public SiteModel GetSiteByUrl(string url)
        {
            var site = _siteRepository.GetSiteByUrl(url);

            var siteModel = _mapper.Map<SiteModel>(site);

            return siteModel;
        }

        public List<TestingPageModel> GetMinMax(List<TestingPageModel> pages)
        {
            foreach (var page in pages)
            {
                if (page.Speeds.Count < 2)
                {
                    page.MinResponseTime = 0;
                    page.MaxResponseTime = page.CurrentResponseTime;
                }
                else
                {
                    if (page.Speeds[0].Speed == 0)
                    {
                        page.MinResponseTime = page.Speeds[1].Speed;
                    }
                    page.MinResponseTime = page.Speeds[0].Speed;
                    page.MaxResponseTime = page.Speeds[0].Speed;

                    for (int i = 0; i < page.Speeds.Count; i++)
                    {
                        if (page.Speeds[i].Speed < page.MinResponseTime)
                        {
                            page.MinResponseTime = page.Speeds[i].Speed;
                        }

                        if (page.Speeds[i].Speed > page.MaxResponseTime)
                        {
                            page.MaxResponseTime = page.Speeds[i].Speed;
                        }
                    }
                }
            }

            return pages;
        }


        public SiteModel GetSiteById(int id)
        {
            var site = _siteRepository.GetSiteById(id);

            var siteModel = _mapper.Map<SiteModel>(site);

            return siteModel;
        }
        private XmlDocument DownloadXMLDocument(string sitemap)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sitemap);
                request.UserAgent = "My User Agent";

                // Load data  
                doc.Load(request.GetResponse().GetResponseStream());
            }
            catch
            {
                return null;
            }

            return doc;
        }

        public List<TestingPageModel> MeasureSpeedResponse(string sitemap)
        {
            var measures = new List<TestingPageModel>();
            var xmlDoc = DownloadXMLDocument(sitemap);

            if (xmlDoc == null)
            {
                measures = GetSpeedsForCreatedSitemap(sitemap);
            }

            else
            {
                measures = GetSpeedsForExistingSitemap(xmlDoc);
            }

            return measures;
        }

        public List<TestingPageModel> GetListSpeeds(List<TestingPageModel> measures, List<TestingPageModel> pagesSite)
        {

            var speed = new PageSpeedModel();
            speed.Speed = 0;
            for (int i = 0; i < measures.Count; i++)
            {
                if (measures[i].Speeds.Count == 0)
                {
                    pagesSite[i].Speeds.Add(speed);
                }
                else
                {
                    pagesSite[i].Speeds.Add(measures[i].Speeds[0]);
                    pagesSite[i].CurrentResponseTime = measures[i].CurrentResponseTime;
                }
            }

            return pagesSite;
        }

        public void UpdateSite(List<TestingPageModel> pagesSiteModel, int id)
        {
            var pagesSite = _mapper.Map<List<TestingPage>>(pagesSiteModel);
            _siteRepository.UpdateSite(pagesSite, id);
        }

        private List<string> PageParser(string url)
        {
            var listLinks = new List<string>();

            WebClient client = new WebClient();
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string html = reader.ReadToEnd();
            data.Close();
            reader.Close();

            HtmlDocument pageHtml = new HtmlDocument();
            pageHtml.LoadHtml(html);

            foreach (HtmlNode link in pageHtml.DocumentNode.SelectNodes("//a[@href]"))
            {
                // Get the value of the HREF attribute
                string hrefValue = link.GetAttributeValue("href", string.Empty);

                if ((hrefValue.Length > 2) && hrefValue.StartsWith(url))
                {
                    listLinks.Add(hrefValue);
                }
            }

            return listLinks;
        }

        private List<TestingPageModel> GetSpeedsForCreatedSitemap(string sitemap)
        {
            int endPartUrl = 12;
            var measures = new List<TestingPageModel>();
            var url = sitemap.Substring(0, sitemap.Length - endPartUrl);
            var pages = PageParser(url);

            foreach (var page in pages)
            {
                var currentPage = new TestingPageModel();
                currentPage.PageUrl = page;

                try
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(currentPage.PageUrl);
                    //request.UserAgent = "My User Agent";
                     
                    HttpWebResponse response =  (HttpWebResponse)request.GetResponse();
                    
                    timer.Stop();
                    currentPage.CurrentResponseTime = timer.Elapsed.TotalMilliseconds;
                    currentPage.Speeds.Add(new PageSpeedModel { Speed = currentPage.CurrentResponseTime });
                }
                catch
                {
                    currentPage.CurrentResponseTime = 0;
                }

                measures.Add(currentPage);
            }
            return measures;
        }

        private List<TestingPageModel> GetSpeedsForExistingSitemap(XmlDocument xmlDoc)
        {
            var measures = new List<TestingPageModel>();
            XmlElement xRoot = xmlDoc.DocumentElement;

            foreach (XmlNode xnode in xRoot)
            {
                foreach (XmlNode childNodes in xnode.ChildNodes)
                {
                    if (childNodes.Name == "loc")
                    {
                        var currentPage = new TestingPageModel();
                        currentPage.PageUrl = childNodes.InnerText;

                        try
                        {
                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(currentPage.PageUrl);
                            //request.UserAgent = "My User Agent";

                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                            timer.Stop();
                            currentPage.CurrentResponseTime = timer.Elapsed.TotalMilliseconds;
                            currentPage.Speeds.Add(new PageSpeedModel { Speed = currentPage.CurrentResponseTime });
                        }
                        catch
                        {
                            currentPage.CurrentResponseTime = 0;
                        }

                        measures.Add(currentPage);
                    }
                }
            }
            return measures;
        }
    }

}



