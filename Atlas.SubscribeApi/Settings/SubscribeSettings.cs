﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.SubscribeApi.Settings
{
    public class SubscribeSettings
    {
        public const string SubscribeSection = "Subscribe";

        public string Url { get; set; }

        public string AuthToken { get; set; }
    }
}
