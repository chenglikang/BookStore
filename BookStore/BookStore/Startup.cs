﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BookStore.Startup))]
namespace BookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
