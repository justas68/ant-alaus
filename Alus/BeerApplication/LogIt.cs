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

namespace BeerApplication
{
    class LogIt
    {
        ISimpleMatasLogger logger;

        public LogIt(ISimpleMatasLogger logger)
        {
            this.logger = logger;
        }

        public void Log(string text)
        {
            logger.LogToLogger(text);
        }
    }
}