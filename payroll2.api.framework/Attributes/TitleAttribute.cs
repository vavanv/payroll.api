//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Text;

//namespace Payroll2.Api.Framework.Attributes
//{
//    public class TitleAttribute : Attribute
//    {
//        private readonly string _resourceKey;

//        public string Title
//        {
//            [DebuggerStepThrough]
//            get
//            {
//                string temp;
//                try
//                {
//                    temp = SystemResource.GetFrameworkResource(this._resourceKey, TitleResources.ResourceManager);
//                    if (string.IsNullOrEmpty(temp))
//                    {
//                        temp = _resourceKey;//return the key value if not defined in resource file
//                    }
//                }
//                catch
//                {
//                    temp = _resourceKey;
//                }
//                return temp;
//            }
//        }

//        public TitleAttribute(string resourceKey)
//        {
//            this._resourceKey = resourceKey;
//        }

//        public int Order { get; set; }

//        public static string GetTitle(Enum value)
//        {
//            var type = value.GetType();
//            FieldInfo fieldInfo = type.GetField(value.ToString());
//            var attribute = (TitleAttribute)fieldInfo.GetCustomAttributes(typeof(TitleAttribute), true).SingleOrDefault();
//            return (attribute == null ? string.Empty : attribute.Title);
//        }
//    }
//}

using System;