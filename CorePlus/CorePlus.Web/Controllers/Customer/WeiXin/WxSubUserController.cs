using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;

namespace CorePlus.Web
{
    public class WxSubUserController : WxUserNameController<WxSubUserRepository, WxSubUserEntity>
    {

    }
}