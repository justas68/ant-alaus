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
using System.IO;

namespace BeerApplication
{
    public class SimpleMatasFileLogger : ISimpleMatasLogger
    {



        public void LogToLogger(string text)
        {
            /*File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + "log.txt",
                  text);*/

        }
    }
}