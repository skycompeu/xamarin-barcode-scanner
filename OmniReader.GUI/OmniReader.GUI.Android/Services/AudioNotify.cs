using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OmniReader.Core.EMDK;
using OmniReader.GUI.Droid.Services;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(AudioNotify))]
namespace OmniReader.GUI.Droid.Services
{
    public class AudioNotify : IAudioNotify
    {
        private MediaPlayer m_Player;
        
        public AudioNotify() { }
        
        private string GetPath(string fileName) {
            return "";
        }

        public void Play(int resourceId)
        {
            m_Player = MediaPlayer.Create(global::Android.App.Application.Context, resourceId);
            
            if (m_Player.IsPlaying) {
                m_Player.Stop();
                m_Player.Reset();
            }

            //m_Player.Dispose

            m_Player.Start();
        }
        
        public void Ok()
        {
            try
            {
                Play(Resource.Raw.ok);
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                
            }
        }
        
        public void NoUnique()
        {
            
        }

        public void Error()
        {
            
        }

        

        
























        //public bool Error()
        //{

        //    m_Player = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.error);
        //    m_Player.Start();
        //    m_Player.Release();
        //    return true;
        //}

        //public bool NonUnique()
        //{
        //    m_Player = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.non_unique);
        //    m_Player.Start();
        //    m_Player.Release();
        //    return true;
        //}

        //public bool NonUniqueV2()
        //{
        //    m_Player = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.non_uniquev2);
        //    m_Player.Start();
        //    m_Player.Release();
        //    return true;
        //}

        //public bool Ok()
        //{
        //    m_Player = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.scan_ok);
        //    m_Player.Start();
        //    m_Player.Release();

        //    return true;
        //}
    }
}