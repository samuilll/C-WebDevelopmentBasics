using SIS.WebServer;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework
{
   public static class MvcEngine
    {
        public static void Run(Server server)
        {
            RegisterAssemblyName();
            RegisterControllersData();
            RegisterViewsData();
            RegisterModelsData();
            RegisterRootDirectoryRelativePath();
            RegisterHtmlFileExtension();;

            try
            {
                server.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RegisterHtmlFileExtension()
        {
            MvcContext.Get.HtmlFileExtension = "html";
        }

        private static void RegisterRootDirectoryRelativePath()
        {
            MvcContext.Get.RootDirectoryRelativePath = @"..\..\..";
        }

        private static void RegisterAssemblyName()
        {
            MvcContext.Get.AssemblyName =
                Assembly.GetEntryAssembly().GetName().Name;
        }

        private static void RegisterModelsData()
        {
            MvcContext.Get.ModelsFolder = "Models";
        }

        private static void RegisterViewsData()
        {
            MvcContext.Get.ViewsFolder = "Views";
        }

        private static void RegisterControllersData()
        {
            MvcContext.Get.ControllersFolder = "Controllers";
            MvcContext.Get.ControllerSuffix = "Controller";
        }
    }
}
