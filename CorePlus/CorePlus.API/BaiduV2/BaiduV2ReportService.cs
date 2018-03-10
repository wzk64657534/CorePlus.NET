using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;

namespace CorePlus.API
{
    public class BaiduV2ReportService
    {
        BaiduAPI_V2_ReportService.ReportServiceClient service;
        BaiduAPI_V2_ReportService.AuthHeader header;
        BaiduAPI_V2_ReportService.ResHeader resHeader;

        public BaiduV2ReportService(string userName, string passWord, string toKen)
        {
            service = new BaiduAPI_V2_ReportService.ReportServiceClient();
            header = new BaiduAPI_V2_ReportService.AuthHeader();
            resHeader = new BaiduAPI_V2_ReportService.ResHeader();
            header.username = userName;
            header.password = passWord;
            header.token = toKen;
        }

        public int GetReportState(string reportId)
        {
            int i = -1;
            resHeader = service.getReportState(header, reportId, out i);
            return i;
        }

        public bool HasReportOnServer(string reportId)
        {
            return GetReportState(reportId) == 3;
        }

        public FilePathInfo GetReportFileUrl(string reportId)
        {
            string reportFilePath = null;
            resHeader = service.getReportFileUrl(header, reportId, out reportFilePath);
            FilePathInfo fpi = new FilePathInfo();
            fpi.FilePath = reportFilePath;
            fpi.FileName = FileHelper.GetFileNameFromUrlForStatistics(reportFilePath) + ".txt";
            return fpi;
        }

        public string GetProfessionalReportId(string[] performanceData, int reportType, int levelOfDetails, int statRange, DateTime startDate, DateTime endDate)
        {
            BaiduAPI_V2_ReportService.ReportRequestType requestType = new BaiduAPI_V2_ReportService.ReportRequestType();
            requestType.performanceData = performanceData;
            requestType.startDate = Convert.ToDateTime(startDate.ToString("yyyy-MM-ddT00:00:00.000"));
            requestType.endDate = Convert.ToDateTime(endDate.ToString("yyyy-MM-ddT23:59:59.000"));
            requestType.unitOfTime = 5;
            requestType.format = 1;
            requestType.platform = 0;
            requestType.statIds = null;
            requestType.attributes = null;
            requestType.idOnly = false;
            requestType.reportType = reportType;
            requestType.levelOfDetails = levelOfDetails;
            requestType.statRange = statRange;

            string reportId = null;
            resHeader = service.getProfessionalReportId(header, requestType, out reportId);
            return reportId;
        }

        public string GetReportForAccount()
        {
            return GetReportForAccount(DateTimeHelper.Yesterday);
        }

        public string GetReportForAccount(DateTime someDay)
        {
            return GetProfessionalReportId(ConstHelper.BaiduV2AccountPerformanceData, 2, 2, 2, someDay, someDay);
        }

        public string GetReportForCampaign()
        {
            return GetReportForCampaign(DateTimeHelper.Yesterday);
        }

        public string GetReportForCampaign(DateTime someDay)
        {
            return GetProfessionalReportId(ConstHelper.BaiduV2CampaignPerformanceData, 10, 3, 3, someDay, someDay);
        }

        public string GetReportForAdgroup()
        {
            return GetReportForAdgroup(DateTimeHelper.Yesterday);
        }

        public string GetReportForAdgroup(DateTime someDay)
        {
            return GetProfessionalReportId(ConstHelper.BaiduV2AdgroupPerformanceData, 11, 5, 5, someDay, someDay);
        }

        public string GetReportForKeyword()
        {
            return GetReportForKeyword(DateTimeHelper.Yesterday);
        }

        public string GetReportForKeyword(DateTime someDay)
        {
            return GetProfessionalReportId(ConstHelper.BaiduV2KeywordPerformanceData, 14, 11, 11, someDay, someDay);
        }

        public string GetReportForCreative()
        {
            return GetReportForCreative(DateTimeHelper.Yesterday);
        }

        public string GetReportForCreative(DateTime someDay)
        {
            return GetProfessionalReportId(ConstHelper.BaiduV2CreativePerformanceData, 12, 7, 7, someDay, someDay);
        }
    }
}