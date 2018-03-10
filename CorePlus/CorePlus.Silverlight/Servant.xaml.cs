using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Browser;

namespace CorePlus.Silverlight
{
    public partial class Servant : BaseDialog
    {
        ServantService.ServantInfoEntity ServantEntity = null;
        LoginWindow login;

        public Servant()
            : base()
        {

        }

        protected override void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //login = new LoginWindow();
            //login.Closed += new EventHandler(Login_Closed);
            //login.Show();

            if (!CookieHelper.Exists("UserId")
               && !CookieHelper.Exists("HighId"))
            {
                login = new LoginWindow();
                login.Closed += new EventHandler(Login_Closed);
                login.Show();
            }
            else
            {
                var userId = CookieHelper.GetCookie("UserId");
                var owner = CookieHelper.GetCookie("HighId");
                ServantService.ServantServiceSoapClient service = new ServantService.ServantServiceSoapClient();
                service.GetByOwnerAsync(userId, owner);
                service.GetByOwnerCompleted +=
                    new EventHandler<ServantService.GetByOwnerCompletedEventArgs>(Service_GetByOwnerCompleted);
            }
        }

        private void Login_Closed(object sender, EventArgs e)
        {
            try
            {
                if (login.DialogResult ?? false)
                {
                    ServantEntity = login.ResultEntity;
                    Sender = ServantEntity.UserName.ToString();
                    Owner = ServantEntity.UserId.ToString();
                    Identity = "SERVANT";
                    Connection();
                }
            }
            catch (Exception ex)
            {
                OutPut(ex.InnerException.Message);
            }
        }

        private void Service_GetByOwnerCompleted(object sender, ServantService.GetByOwnerCompletedEventArgs e)
        {
            try
            {
                ServantEntity = e.Result;
                Sender = ServantEntity.UserName.ToString();
                Owner = ServantEntity.UserId.ToString();
                Identity = "SERVANT";

                Connection();
            }
            catch (Exception ex)
            {
                OutPut(ex.InnerException.Message);
            }
        }
    }
}