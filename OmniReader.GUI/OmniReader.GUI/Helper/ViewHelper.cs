using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;

namespace OmniReader.GUI.Helper
{
    public static class ViewHelper
    {
        public static void SetAppGlobalTheme(ResourceDictionary resources, string version)
        {
            version = $"OmniReader.GUI.Assets.Style.Theme.AppThemeV{version}.css";

            //"MyProject.Assets.styles.css"
            resources.Add(StyleSheet.FromAssemblyResource(
            IntrospectionExtensions.GetTypeInfo(typeof(OmniReader.GUI.App)).Assembly, version));
        }
        
        public static void SetPageStyle(ResourceDictionary resources, string styleName)
        {
            styleName = $"OmniReader.GUI.Assets.Style.Page.{styleName}.css";
            resources.Add(StyleSheet.FromAssemblyResource(
            IntrospectionExtensions.GetTypeInfo(typeof(OmniReader.GUI.App)).Assembly, styleName));
        }
    }
}