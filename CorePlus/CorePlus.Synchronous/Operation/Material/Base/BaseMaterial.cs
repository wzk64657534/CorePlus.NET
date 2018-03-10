using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;

using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public abstract class BaseMaterial : IMaterial
    {
        public void Update(SynCheckedDataInfoEntity entity)
        {
            LogInfoEntity log = new LogInfoEntity() { AccountName = entity.AccountName, Operation = GetOperation(), ErrorDate = DateTime.Now, Tag = entity.DealWithId };
            try
            {
                string fileContent = string.Empty;
                string filePath = PathHelper.GetDirectoryName("Files") + entity.FileName;

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    {
                        fileContent = sr.ReadToEnd();
                    }
                }

                if (string.IsNullOrWhiteSpace(fileContent) == false)
                {
                    string[] rows = fileContent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    if (rows.Length > 1)
                    {
                        // 第一行是标题，从第2行开始读
                        for (int i = 1; i < rows.Length; i++)
                        {
                            string row = rows[i];
                            string[] fields = row.Split('\t');
                            DealRowData(fields, entity);
                        }
                        // 读取完成，删除缓存、数据库数据、文件
                        DataHelper.RemoveSynCheckedDataInfo(entity);
                        File.Delete(filePath);

                        log.ErrorMsg = "同步完成";
                        ParamHelper.wcfLog.Add(log);
                    }
                    else
                    {
                        // 只有标题，没有谁数据，删掉文件和记录
                        DataHelper.RemoveSynCheckedDataInfo(entity);
                        File.Delete(filePath);

                        log.ErrorMsg = "因为没有数据而删除";
                        ParamHelper.wcfLog.Add(log);
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorMsg = ex.Message;
                // 找不到文件删除记录
                DataHelper.RemoveSynCheckedDataInfo(entity);
                ParamHelper.wcfLog.Add(log);
            }
        }

        protected abstract string GetOperation();

        protected abstract void DealRowData(string[] fields, SynCheckedDataInfoEntity entity);
    }
}