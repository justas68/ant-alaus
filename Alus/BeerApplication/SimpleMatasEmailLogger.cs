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
    public class SimpleMatasEmailLogger : Activity, ISimpleMatasLogger
    {


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }

        public void LogToLogger(string text)
        {
            /*var email = new Intent(Android.Content.Intent.);
            email.PutExtra(Android.Content.Intent.ExtraEmail, new string[] { "savickismatas@gmail.com" });
            email.PutExtra(Android.Content.Intent.ExtraSubject,"savickismatas@gmail.com");
            email.PutExtra(Android.Content.Intent.ExtraText, "Test");
            email.SetType("message/rfc822");
            StartActivity(email);*/
        }
    }
}