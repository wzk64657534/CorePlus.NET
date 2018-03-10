using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Memcached.ClientLibrary;
using System.Configuration;
using Core;

namespace CorePlus.PrimaryKey
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            char[] separator = { ',' };
            string[] serverlist = ConfigurationManager.AppSettings["Memcached.ServerList"].Split(separator);

            try
            {
                SockIOPool pool = SockIOPool.GetInstance();
                pool.SetServers(serverlist);

                pool.InitConnections = 3;
                pool.MinConnections = 3;
                pool.MaxConnections = 50;

                pool.SocketConnectTimeout = 1000;
                pool.SocketTimeout = 3000;

                pool.MaintenanceSleep = 30;
                pool.Failover = true;

                pool.Nagle = false;
                pool.Initialize();

                // 在应用程序启动时运行的代码
                MemcachedClient client = new MemcachedClient();
                if (client.KeyExists("PrimaryKeyProvider"))
                {
                    client.Set("PrimaryKeyProvider", new PrimaryKeyProvider());
                }
                else
                {
                    client.Add("PrimaryKeyProvider", new PrimaryKeyProvider());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log4Net.Error(ex.Message);
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

    }
}
