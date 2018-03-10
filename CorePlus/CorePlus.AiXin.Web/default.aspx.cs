using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace CorePlus.AiXin.Web
{
    public partial class _default : System.Web.UI.Page
    {
        static string HMACMD5KEY = "{DC1142CB-7687-4A99-9FB8-4A9BD749038E}";
        static string UserID = "00000";
        static string Password = "501ae668de56c561f98b68b2c1c50e6b";
        static string CompanyCode = "51900000";
        static string ShopCode = "001";
        static string PosCode = "005";
        static string PosMark = "AC4FB26212D3F13997E7BF041556ECA8";
        static string LoginSign = "33F6BAEEB0DB60C90824153662CFD454501c4f7bbd3fb63a9ac3170e1e3ce846";
        static StringBuilder AParamsData = new StringBuilder();
        static AiXinMobileServerService.AixinMobileServerClient client = new AiXinMobileServerService.AixinMobileServerClient();

        private void ActionToExecute(StringBuilder AParamsData)
        {
            string actionName = StringHelper.Extract(AParamsData.ToString(), "<ActionName>", "</ActionName>");
            AParamsData.AppendFormat("<LoginSign>{0}</LoginSign>", LoginSign);
            string APassword = CryptHelper.HMAC_MD5(AParamsData.ToString(), HMACMD5KEY);
            string data = string.Empty;

            switch (actionName)
            {
                case "LoginSign":
                    data = client.DimooActionUserLogin(AParamsData.ToString(), APassword);
                    break;
                default:
                    data = client.DimooActionToExecute(AParamsData.ToString(), APassword);
                    break;
            }

            this.txbMsg.Text = data;
        }

        private void InitHead()
        {
            AParamsData.Clear();
            AParamsData.AppendFormat("<UserID>{0}</UserID>", UserID);
            AParamsData.AppendFormat("<Password>{0}</Password>", Password);
            AParamsData.AppendFormat("<CompanyCode>{0}</CompanyCode>", CompanyCode);
            AParamsData.AppendFormat("<ShopCode>{0}</ShopCode>", ShopCode);
            AParamsData.AppendFormat("<PosCode>{0}</PosCode>", PosCode);
            AParamsData.AppendFormat("<PosMark>{0}</PosMark>", PosMark);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ActionToExecute_Click(object sender, EventArgs e)
        {
            InitHead();
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "btnHMAC_MD5":
                    this.txbMsg.Text = CryptHelper.HMAC_MD5(this.txbMsg.Text.Replace(" ", ""), HMACMD5KEY);
                    return;
                case "btnHMAC_DES":
                    this.txbMsg.Text = CryptHelper.HMAC_DES(this.txbMsg.Text.Replace(" ", ""), "12345678");
                    return;
            }

            ActionToExecute(AParamsData);
        }
    }
}