using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.API
{
    public class BaiduV2FolderService
    {
        BaiduAPI_V2_FolderService.FolderServiceClient service;
        BaiduAPI_V2_FolderService.AuthHeader header;
        BaiduAPI_V2_FolderService.ResHeader resHeader;

        public BaiduV2FolderService(string userName, string passWord, string toKen)
        {
            service = new BaiduAPI_V2_FolderService.FolderServiceClient();
            header = new BaiduAPI_V2_FolderService.AuthHeader();
            resHeader = new BaiduAPI_V2_FolderService.ResHeader();
            header.username = userName;
            header.password = passWord;
            header.token = toKen;
        }

        public BaiduAPI_V2_FolderService.Folder[] GetFolderAll()
        {
            BaiduAPI_V2_FolderService.Folder[] folders;
            resHeader = service.getFolder(header, null, out folders);
            return folders;
        }

        public BaiduAPI_V2_FolderService.FolderMonitor[] GetMonitorAll()
        {
            BaiduAPI_V2_FolderService.FolderMonitor[] models;
            resHeader = service.getMonitorWordByFolderId(header, null, out models);
            return models;
        }
    }
}