﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fpluswebsite2.Startup))]
namespace fpluswebsite2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
