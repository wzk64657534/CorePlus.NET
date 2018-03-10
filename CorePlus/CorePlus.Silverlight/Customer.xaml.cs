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
using System.IO.IsolatedStorage;
using System.Windows.Browser;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace CorePlus.Silverlight
{
    public partial class Customer : BaseDialog
    {
        public Customer()
            : base()
        {

        }

        protected override void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Sender = new Random().Next(1000000001, 1999999999).ToString();
            Identity = "CUSTOMER";
            //Owner = "1";
            Owner = HtmlPage.Document.QueryString["hid"];
            base.Page_Loaded(sender, e);
        }
    }
}