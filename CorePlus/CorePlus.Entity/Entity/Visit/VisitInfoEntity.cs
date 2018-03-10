using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Core;

namespace CorePlus.Entity
{
    [Table("VisitInfo")]
    public class VisitInfoEntity : BaseEntity
    {
        public Nullable<long> UserId { get; set; }
        public string ConfigOS { get; set; }
        public string ConfigBrowserName { get; set; }
        public string ConfigBrowserVersion { get; set; }
        public string ConfigBrowserLang { get; set; }
        public string ConfigResolution { get; set; }
        public string RefererType { get; set; }
        public string RefererName { get; set; }
        public string RefererUrl { get; set; }
        public string RefererKeyword { get; set; }
        public string RefererSite { get; set; }
        public string RefererPage { get; set; }
        public string LocationIP { get; set; }
        public string LocationCountry { get; set; }
        public string LocationRegion { get; set; }
        public string LocationCity { get; set; }
        public Nullable<decimal> LocationLatitude { get; set; }
        public Nullable<decimal> LocationLongitude { get; set; }
        public Nullable<DateTime> VisitTime { get; set; }
        public string VisitingUrl { get; set; }
        public string VisitingSite { get; set; }
        public Nullable<decimal> VisitPeriodTime { get; set; }
        public string VisitId { get; set; }
        public string LoginPage { get; set; }
        public int? VisitType { get; set; }
    }
}