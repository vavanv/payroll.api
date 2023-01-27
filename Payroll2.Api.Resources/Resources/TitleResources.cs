//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Resources;
//using System.Text;

//namespace Payroll2.Api.Resources.Resources
//{
//    public static class TitleResources
//    {
//        public static string GetTitleResource(string key, ResourceManager resourceManager,
//            CultureInfo cultureInfo = null)
//        {
//            if (String.IsNullOrEmpty(key) || resourceManager == null)
//            {
//                return null;
//            }

//            var defaultValue = resourceManager.GetString(key, cultureInfo);
//            if (ApplicationEnvironment.Container == null)
//            {
//                return defaultValue;
//            }
//            try
//            {
//                var systemResourceManager = ApplicationEnvironment.Container.Resolve<ISystemResourceManager>();
//                var resourceKey = ResourceKeys.Static(key, resourceManager);
//                return cultureInfo == null ? systemResourceManager.GetString(resourceKey, defaultValue) : systemResourceManager.GetString(cultureInfo, resourceKey, defaultValue);
//            }
//            catch
//            {
//                return defaultValue;
//            }
//        }
//    }
//}

using System;