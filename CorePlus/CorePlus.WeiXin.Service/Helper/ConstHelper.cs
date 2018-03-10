using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class ConstHelper
    {
        public const string WEIXIN_ACCESS_TOKEN = "https://api.weixin.qq.com/cgi-bin/token";//?grant_type=client_credential&appid={0}&secret={1}";
        public const string CURD_MENU_CREATE = "https://api.weixin.qq.com/cgi-bin/menu/create";//?access_token={0}";
        public const string CURD_MENU_QUERY = "https://api.weixin.qq.com/cgi-bin/menu/get";//?access_token={0}";
        public const string CURD_MENU_DELETE = "https://api.weixin.qq.com/cgi-bin/menu/delete";//?access_token={0}";

        public const string CURD_USER_GROUP_CREATE = "https://api.weixin.qq.com/cgi-bin/groups/create";//?access_token={0}";
        public const string CURD_USER_GROUP_QUERY_ALL = "https://api.weixin.qq.com/cgi-bin/groups/get";//?access_token={0}";
        public const string CURD_USER_GROUP_QUERY_USERID = "https://api.weixin.qq.com/cgi-bin/groups/getid";//?access_token={0}";
        public const string CURD_USER_GROUP_UPDATE_NAME = "https://api.weixin.qq.com/cgi-bin/groups/update";//?access_token={0}";
        public const string CURD_USER_GROUP_MOVE = "https://api.weixin.qq.com/cgi-bin/groups/members/update";//?access_token={0}";
        public const string CURD_USER_GET_BASIC_INFOMATION = "https://api.weixin.qq.com/cgi-bin/user/info";//?access_token={0}&openid={1}";
        public const string CURD_USER_NOTICE_LIST = "https://api.weixin.qq.com/cgi-bin/user/get";//?access_token={0}&next_openid={1}";
        public const string CURD_SPREAD_GET_TICKET = "https://api.weixin.qq.com/cgi-bin/qrcode/create";//?access_token={0}";
        public const string CURD_SPREAD_GET_CODE = "https://mp.weixin.qq.com/cgi-bin/showqrcode";//?ticket={0}";
        public const string CURD_SERVANT_SEND_MESSAGE = "https://api.weixin.qq.com/cgi-bin/message/custom/send";//?access_token={0}";
        public const string CURD_FILE_UPLOAD_MEDIA = "http://file.api.weixin.qq.com/cgi-bin/media/upload";//?access_token={0}&type={1}";
        public const string CURD_FILE_DOWNLOAD_MEDIA = "http://file.api.weixin.qq.com/cgi-bin/media/get";//?access_token={0}&media_id={1}";
        public const string CURD_PAGE_OAUTH_1 = "https://open.weixin.qq.com/connect/oauth2/authorize";//?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect";
        public const string CURD_PAGE_OAUTH_2 = "https://api.weixin.qq.com/sns/oauth2/access_token";//?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        public const string CURD_PAGE_OAUTH_3 = "https://api.weixin.qq.com/sns/oauth2/refresh_token";//?appid={0}&grant_type=refresh_token&refresh_token={1}";
        public const string CURD_PAGE_OAUTH_4 = "https://api.weixin.qq.com/sns/userinfo";//?access_token={0}&openid={1}";

        public const string ERROR_100001 = "{\"errcode\":100001,\"errmsg\":\"缺少参数，必须有操作类型(key)\"}";
        public const string ERROR_100002 = "{\"errcode\":100002,\"errmsg\":\"没有匹配的操作\"}";
        public const string ERROR_100003 = "{\"errcode\":100003,\"errmsg\":\"缺少必要参数({0})\"}";
        public const string ERROR_100004 = "{\"errcode\":100004,\"errmsg\":\"access_token请求超时\"}";
        public const string ERROR_100005 = "{\"errcode\":100005,\"errmsg\":\"缺少参数，必须有商家登录名(un)\"}";
        public const string ERROR_100006 = "{\"errcode\":100006,\"errmsg\":\"缺少参数，必须有商家令牌(unkey)\"}";
        public const string ERROR_100007 = "{\"errcode\":100007,\"errmsg\":\"抱歉，您不是这里的商家\"}";
        public const string ERROR_999998 = "{\"errcode\":999998,\"errmsg\":\"未知错误\"}";
        public const string ERROR_999999 = "{\"errcode\":999999,\"errmsg\":\"异常：{0}\"}";

        public const string FORMAT_TEXT = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>";
        public const string FORMAT_IMAGE_TEXT = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[news]]></MsgType>{3}</xml>";
    }
}