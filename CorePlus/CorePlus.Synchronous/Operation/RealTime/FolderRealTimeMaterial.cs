using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class FolderRealTimeMaterial : IRealTimeMaterial
    {
        public void Update()
        {
            LogHelper.AddLog("开始同步Folder", "FolderRealTimeMaterial");

            var accounts = ParamHelper.wcfAccount.FindAll().ToList();
            foreach (var item in accounts)
            {
                try
                {
                    // 同步文件夹
                    BaiduV2FolderService service = new BaiduV2FolderService(item.AccountName, CryptHelper.DESDecode(item.AccountPwd), CryptHelper.DESDecode(item.Token));
                    var folders = service.GetFolderAll();
                    if (folders != null)
                    {
                        foreach (var folder in folders)
                        {
                            // 判断在系统内是否存在，不存在添加，存在修改
                            var fd = ParamHelper.wcfFolder.FindByID(folder.folderId);
                            if (fd == null)
                            {
                                FolderInfoEntity model = new FolderInfoEntity();
                                model.ID = folder.folderId;
                                model.FolderName = folder.folderName;
                                model.AccountName = item.AccountName;
                                ParamHelper.wcfFolder.Add(model);
                            }
                            else
                            {
                                fd.FolderName = folder.folderName;
                                fd.AccountName = item.AccountName;
                                ParamHelper.wcfFolder.Update(fd);
                            }
                        }

                        // 同步监控对象
                        var monitors = service.GetMonitorAll();
                        if (monitors != null)
                        {
                            foreach (var monitor in monitors)
                            {
                                foreach (var m in monitor.monitors)
                                {
                                    var mn = ParamHelper.wcfMonitor.FindByID(m.monitorId);
                                    if (mn == null)
                                    {
                                        MonitorInfoEntity model = new MonitorInfoEntity();
                                        model.ID = m.monitorId;
                                        model.FolderId = monitor.folderId;
                                        model.AdgroupId = m.adgroupId;
                                        model.CampaignId = m.campaignId;
                                        model.ObjId = m.id;
                                        model.Type = m.type;
                                        ParamHelper.wcfMonitor.Add(model);
                                    }
                                    else
                                    {
                                        mn.FolderId = monitor.folderId;
                                        ParamHelper.wcfMonitor.Update(mn);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(ex.Message, "FolderRealTimeMaterial");
                }
            }
        }
    }
}