using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CorePlus.Silverlight
{
    public partial class LoginWindow : ChildWindow
    {
        public ServantService.ServantInfoEntity ResultEntity { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.lblMsg.Content = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(this.txbUserName.Text) || this.txbUserName.Text.Trim().Length < 6)
                {
                    this.lblMsg.Content = "请输入用户名，6-20位";
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txbPassword.Password) || this.txbPassword.Password.Trim().Length < 6)
                {
                    this.lblMsg.Content = "请输入密码，6-20位";
                    return;
                }

                ServantService.ServantServiceSoapClient service = new ServantService.ServantServiceSoapClient();
                service.LoginAsync(this.txbUserName.Text, CryptHelper.MD5(this.txbPassword.Password));
                service.LoginCompleted += new EventHandler<ServantService.LoginCompletedEventArgs>(Service_LoginCompleted);
            }
            catch (Exception ex)
            {
                this.DialogResult = false;
                MessageBox.Show("异常：" + ex.Message, "警告", MessageBoxButton.OK);
            }
        }

        private void Service_LoginCompleted(object sender, ServantService.LoginCompletedEventArgs e)
        {
            ResultEntity = e.Result;
            if (ResultEntity != null)
            {
                this.DialogResult = true;
                CookieHelper.SetCookie("UserId", ResultEntity.ID.ToString());
                CookieHelper.SetCookie("Owner", ResultEntity.UserId.ToString());
            }
            else
            {
                this.lblMsg.Content = "账号或密码错误，请重新输入";
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.txbUserName.Text = string.Empty;
            this.txbPassword.Password = string.Empty;
            this.lblMsg.Content = string.Empty;
        }
    }
}