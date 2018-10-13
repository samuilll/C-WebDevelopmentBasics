namespace SIS.MvcFramework.Utilities
{
  public static  class ControllerUtilities
  {
      public static string GetControllerName(object Controller)
          => Controller.GetType()
              .Name
              .Replace(MvcContext.Get.ControllerSuffix, string.Empty);

      public static string GetViewFullyQualifiedName(string controller, string action)
          => string.Format(
              @"{0}\{1}\{2}\{3}.{4}",
              MvcContext.Get.RootDirectoryRelativePath,
              MvcContext.Get.ViewsFolder,
              controller,
              action,
              MvcContext.Get.HtmlFileExtension
          );
  }
}
