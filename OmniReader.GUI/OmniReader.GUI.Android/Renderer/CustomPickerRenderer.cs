using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OmniReader.GUI.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(OmniReader.GUI.Renderer.CustomPickerRenderer), typeof(CustomPickerRenderer))]
namespace OmniReader.GUI.Droid.Renderer
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                Control.Gravity = GravityFlags.CenterHorizontal;
                Control.ScrollBarStyle = ScrollbarStyles.InsideInset;
            }
        }
    }
}