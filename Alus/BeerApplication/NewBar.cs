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
    [Activity(Label = "Add new bar to the list", Icon = "@drawable/BeerGlass")]
    public class NewBar : Activity
    {
        SeekBar seekBar;
        TextView seekBarText;
        Button button;
        EditText barName;
        LogIt emailLogger = new LogIt(new SimpleMatasEmailLogger());
        LogIt fileLogger = new LogIt(new SimpleMatasFileLogger());

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.NewBarLayout);

            seekBar = FindViewById<SeekBar>(Resource.Id.seekBarOne);
            seekBarText = FindViewById<TextView>(Resource.Id.textView3);
            button = FindViewById<Button>(Resource.Id.button1);
            barName = FindViewById<EditText>(Resource.Id.editText);

            seekBar.ProgressChanged += SeekBar_ProgressChanged;
            button = FindViewById<Button>(Resource.Id.button1);

            button.Click += delegate
            {
               // fileLogger.Log("Button clicked" + DateTime.Now.ToString("h:mm:ss tt"));
                string name = barName.Text;
                int rank = Int32.Parse(seekBarText.Text);
                if (!MainActivity.barInformation.Value.ContainsKey(name))
                {
                    MainActivity.barInformation.Value.Add(name, rank);
                }
            };
        }

        void SeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
           // emailLogger.Log("SeekBar slided" + DateTime.Now.ToString("h:mm:ss tt"));
            seekBarText.Text = string.Format("{0}", e.Progress);
        }
    }
}