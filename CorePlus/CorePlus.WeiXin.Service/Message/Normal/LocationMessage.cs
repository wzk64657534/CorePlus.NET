﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.WeiXin.Repository;
using CorePlus.WeiXin.Entity;

namespace CorePlus.WeiXin.Service
{
    public class LocationMessage : BaseMessage<WxLocationRepository, WxLocationEntity>
    {
    }
}