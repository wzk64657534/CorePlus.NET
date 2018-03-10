using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class BaseDealWith : IDealWithType
    {
        /// <summary>
        /// 请求处理编号
        /// </summary>
        public void GetDealWithId(DateTime dt, SynDataInfoEntity entity)
        {
            BaiduV2AccountService serviceAccount
                 = new BaiduV2AccountService(entity.AccountName, CryptHelper.DESDecode(entity.AccountPwd), CryptHelper.DESDecode(entity.Token));
            string dealId = string.Empty;
            // 轮询接口，直到有ID返回
            while (string.IsNullOrEmpty(dealId))
            {
                dealId = RequestDealWithId(serviceAccount, dt);
                Thread.Sleep(500);
            }

            if (string.IsNullOrEmpty(dealId) == false)
            {
                entity.DealWithId = dealId;
                DataHelper.AppendSynDataInfo(entity);

                RequestReport(dt, entity);
            }
        }
        /// <summary>
        /// 请求的处理ID
        /// </summary>
        protected virtual string RequestDealWithId(BaiduV2AccountService serviceAccount, DateTime dt)
        {
            return string.Empty;
        }
        /// <summary>
        /// 请求统计数据
        /// </summary>
        protected virtual void RequestReport(DateTime dt, SynDataInfoEntity entity)
        {

        }

        protected void RequestStatistics(DateTime dt, SynDataInfoEntity entity)
        {
            foreach (var key in ParamHelper.MaterialKeys)
            {
                ParamHelper.managerReport.GetReportId(key, dt, entity);
            }
        }

        public void SynData(SynCheckedDataInfoEntity entity)
        {
            string key = GetKeyOfSynData(entity);
            ParamHelper.managerMaterail.Update(key, entity);
        }

        protected virtual string GetKeyOfSynData(SynCheckedDataInfoEntity entity)
        {
            return string.Empty;
        }

        public virtual void CheckedData(SynCheckedDataInfoEntity entity, SynDataInfoEntity item)
        {

        }

        protected virtual void DownloadFileAndSynData(SynCheckedDataInfoEntity entity)
        {
            try
            {
                string directotyName = PathHelper.GetDirectoryName("Files");
                bool b = Core.FileHelper.DownLoadFile(entity.FilePath, directotyName, entity.FileName);
                if (b)
                {
                    entity.IsDownLoad = true;
                    if (entity.FileName.Contains(".zip"))
                    {
                        string filePath = directotyName + entity.FileName;
                        string newFilePath = directotyName;
                        ZipHelper.UnZip(filePath, newFilePath);
                        // 新文件名
                        entity.FileName = entity.FileName.Replace(".zip", ".csv");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message, "DownloadFileAndSynData");
            }
        }
    }
}