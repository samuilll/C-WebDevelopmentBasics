using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SIS.GameStoreApp.Common;
using SIS.Infrastructure;

namespace SIS.GameStoreApp.Controllers
{
   public abstract class BaseController:Controller
   {
       protected IMapper mapper;

       protected IServiceProvider serviceProvider;

        protected BaseController()
        {
             serviceProvider = ConfigureServices();

            mapper = serviceProvider.GetService<IMapper>();
        }

        private static IServiceProvider ConfigureServices()
       {
           var services = new ServiceCollection();

           var mapper = CreateConfiguration();

           services.AddSingleton<IMapper>(mapper);

           var provider = services.BuildServiceProvider();

           return provider;
       }

       private static IMapper CreateConfiguration()
       {
           var config = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(typeof(ProfileMapping));
           }).CreateMapper();

           return config;
       }
    }
}
