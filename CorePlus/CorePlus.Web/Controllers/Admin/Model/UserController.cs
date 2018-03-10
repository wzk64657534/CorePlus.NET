using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Web
{
    public class UserController : AdminBaseController<UserRepository, UserInfoEntity>
    {
         
    }
}