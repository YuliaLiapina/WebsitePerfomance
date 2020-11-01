
namespace WebsitePerfomanceManager.Business.Models
{
    public class PageSpeedModel
    {
        public int Id { get; set; }
        public double Speed { get; set; }
        public int IdTestingPage { get; set; }
        public TestingPageModel Page { get; set; }
    }
}
