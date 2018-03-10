<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CorePlus.AiXin.Web._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>测试接口</title>
    <style type="text/css">
        body
        {
            font-size: 12px;
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txbMsg" runat="server" Rows="20" TextMode="MultiLine"
            Width="800px"></asp:TextBox>
        <br />
        -------------------------------- 应用接口 ---------------------------------
        <table border="0" cellpadding="3" cellspacing="3" width="800">
            <tr>
                <td>
                <asp:Button ID="btnHMAC_MD5" runat="server" Text="HMAC_MD5" OnClick="ActionToExecute_Click" />
                </td>
                <td>
                <asp:Button ID="btnHMAC_DES" runat="server" Text="HMAC_DES" OnClick="ActionToExecute_Click" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
