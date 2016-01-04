using System;

namespace GradFolio.Core.ViewModels
{
    public class PortfolioViewModel
    {
        public bool IsReady { get; set; }
        public PortalOverviewStats Stats { get; set; }
        public PortfolioModel Portfolio { get; set; }
    }
    
    public class PortfolioModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public long RefNum { get; set; }
        public DateTime CreateDate { get; set; }
    }
    

    //public class PortfolioSettingViewModel
    //{
    //    public string SettingType { get; set; }
    //    public IEnumerable<SettingModel> AvailableSettings { get; set; }
    //    public IEnumerable<SettingModel> SelectedSettings { get; set; }
    //    public PostedSetting PostedSettings { get; set; }
    //}

    //public class SettingModel
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public bool IsSelected { get; set; }
    //    public object Tags { get; set; }
    //}

    //public class PostedSetting
    //{
    //    public string[] SettingIds { get; set; }
    //}
}
