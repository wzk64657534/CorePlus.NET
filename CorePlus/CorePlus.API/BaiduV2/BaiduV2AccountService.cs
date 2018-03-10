using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.API
{
    public class BaiduV2AccountService
    {
        BaiduAPI_V2_AccountService.AccountServiceClient service;
        BaiduAPI_V2_AccountService.AuthHeader header;
        BaiduAPI_V2_AccountService.ResHeader resHeader;

        public BaiduV2AccountService(string userName, string passWord, string toKen)
        {
            service = new BaiduAPI_V2_AccountService.AccountServiceClient();
            header = new BaiduAPI_V2_AccountService.AuthHeader();
            resHeader = new BaiduAPI_V2_AccountService.ResHeader();
            header.username = userName;
            header.password = passWord;
            header.token = toKen;
        }

        public BaiduAPI_V2_AccountService.AccountInfoType IsExists(string userName, string passWord, string toKen, int type)
        {
            header.username = userName;
            header.password = passWord;
            header.token = toKen;

            BaiduAPI_V2_AccountService.AccountInfoType account;
            resHeader = service.getAccountInfo(header, type, out account);
            return account;
        }

        public BaiduAPI_V2_AccountService.AccountInfoType GetAccountInfo()
        {
            BaiduAPI_V2_AccountService.AccountInfoType account = new BaiduAPI_V2_AccountService.AccountInfoType();
            resHeader = service.getAccountInfo(header, 1, out account);
            return account;
        }
        /// <summary>
        /// 获取文件状态值
        /// </summary>
        public int GetFileState(string fileId)
        {
            int returnValue = -1;
            resHeader = service.getFileState(header, fileId, out returnValue);
            return returnValue;
        }
        /// <summary>
        /// 判断文档是否已经在服务器端生成
        /// </summary>
        public bool HasFileOnServer(string fileId)
        {
            int i = this.GetFileState(fileId);
            return i == 3;
        }
        /// <summary>
        /// 返回账户下所有信息资料（计划、单元、关键字、创意、附加创意等）
        /// </summary>
        public string GetAllObjects()
        {
            string fileId = string.Empty;
            resHeader = service.getAllObjects(header, null, true, true, 0, null, 1, 1, out fileId);
            return fileId;
        }
        /// <summary>
        /// 获得所有文件下载路径
        /// </summary>
        public List<FilePathInfo> GetAllObjectsPath(string fileId)
        {
            string accountFilePath = string.Empty;
            string accountFileMd5 = string.Empty;
            string campaignFilePath = string.Empty;
            string campaignFileMd5 = string.Empty;
            string adgroupFilePath = string.Empty;
            string adgroupFileMd5 = string.Empty;
            string keywordFilePath = string.Empty;
            string keywordFileMd5 = string.Empty;
            string creativeFilePath = string.Empty;
            string creativeFileMd5 = string.Empty;
            string[] newCreativeFilePaths;
            string[] newCreativeFileMd5s;

            resHeader = service.getAllObjectsPath(header, fileId,
                    out accountFilePath, out accountFileMd5,
                    out campaignFilePath, out campaignFileMd5,
                    out adgroupFilePath, out adgroupFileMd5,
                    out keywordFilePath, out keywordFileMd5,
                    out creativeFilePath, out creativeFileMd5,
                    out newCreativeFilePaths, out newCreativeFileMd5s);

            List<FilePathInfo> urls = new List<FilePathInfo>();
            string fileName = string.Empty;
            if (string.IsNullOrWhiteSpace(accountFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(accountFilePath);
                urls.Add(new FilePathInfo() { FilePath = accountFilePath, FileName = fileName });
            }

            if (string.IsNullOrWhiteSpace(campaignFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(campaignFilePath);
                urls.Add(new FilePathInfo() { FilePath = campaignFilePath, FileName = fileName });
            }

            if (string.IsNullOrWhiteSpace(adgroupFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(adgroupFilePath);
                urls.Add(new FilePathInfo() { FilePath = adgroupFilePath, FileName = fileName });
            }

            if (string.IsNullOrWhiteSpace(keywordFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(keywordFilePath);
                urls.Add(new FilePathInfo() { FilePath = keywordFilePath, FileName = fileName });
            }

            if (string.IsNullOrWhiteSpace(creativeFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(creativeFilePath);
                urls.Add(new FilePathInfo() { FilePath = creativeFilePath, FileName = fileName });
            }

            if (newCreativeFilePaths != null && newCreativeFilePaths.Length > 0)
            {
                int cnt = newCreativeFilePaths.Length;
                for (int i = 0; i < cnt; i++)
                {
                    fileName = FileHelper.GetFileNameFromUrlForInfo(newCreativeFilePaths[i]);
                    urls.Add(new FilePathInfo() { FilePath = newCreativeFilePaths[i], FileName = fileName });
                }
            }

            return urls;
        }
        /// <summary>
        /// 获得账户下所有有变化的物料
        /// </summary>
        public string GetAllChangedObjects(DateTime startDate)
        {
            string fileId = null;
            resHeader = service.getAllChangedObjects(header, startDate, null, true, true, true, true, true, 0, null, 1, 1, out fileId);
            return fileId;
        }
        /// <summary>
        /// 获得所有有变化的文件下载路径
        /// </summary>
        public List<FilePathInfo> GetAllChangeObjectsPath(string fileId)
        {
            string campaignFilePath = string.Empty;
            string campaignFileMd5 = string.Empty;
            string adgroupFilePath = string.Empty;
            string adgroupFileMd5 = string.Empty;
            string keywordFilePath = string.Empty;
            string keywordFileMd5 = string.Empty;
            string creativeFilePath = string.Empty;
            string creativeFileMd5 = string.Empty;
            string[] newCreativeFilePaths;
            string[] newCreativeFileMd5s;

            resHeader = service.getAllChangedObjectsPath(header, fileId,
                    out campaignFilePath, out campaignFileMd5,
                    out adgroupFilePath, out adgroupFileMd5,
                    out keywordFilePath, out keywordFileMd5,
                    out creativeFilePath, out creativeFileMd5,
                    out newCreativeFilePaths, out newCreativeFileMd5s);


            List<FilePathInfo> urls = new List<FilePathInfo>();
            string fileName = string.Empty;
            if (string.IsNullOrWhiteSpace(campaignFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(campaignFilePath);
                urls.Add(new FilePathInfo() { FilePath = campaignFilePath, FileName = fileName });
            }

            if (string.IsNullOrWhiteSpace(adgroupFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(adgroupFilePath);
                urls.Add(new FilePathInfo() { FilePath = adgroupFilePath, FileName = fileName });
            }

            if (string.IsNullOrWhiteSpace(keywordFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(keywordFilePath);
                urls.Add(new FilePathInfo() { FilePath = keywordFilePath, FileName = fileName });
            }

            if (string.IsNullOrWhiteSpace(creativeFilePath) == false)
            {
                fileName = FileHelper.GetFileNameFromUrlForInfo(creativeFilePath);
                urls.Add(new FilePathInfo() { FilePath = creativeFilePath, FileName = fileName });
            }

            if (newCreativeFilePaths != null && newCreativeFilePaths.Length > 0)
            {
                int cnt = newCreativeFilePaths.Length;
                for (int i = 0; i < cnt; i++)
                {
                    fileName = FileHelper.GetFileNameFromUrlForInfo(newCreativeFilePaths[i]);
                    urls.Add(new FilePathInfo() { FilePath = newCreativeFilePaths[i], FileName = fileName });
                }
            }

            return urls;
        }
    }
}