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
    [Activity(Label = "Selected bar", Icon = "@drawable/BeerGlass")]
    class SelectedBar : Activity
    {
        TextView barName;
        TextView barEvaluation;
        SeekBar newEvaluation;
        TextView newEvaluationValue;
        Button changeEvaluation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectedBarLayout);

            barName = FindViewById<TextView>(Resource.Id.barName);
            barEvaluation = FindViewById<TextView>(Resource.Id.barEvaluation);
            newEvaluation = FindViewById<SeekBar>(Resource.Id.newEvaluation);
            newEvaluationValue = FindViewById<TextView>(Resource.Id.newEvaluationValue);
            changeEvaluation = FindViewById<Button>(Resource.Id.changeEvaluation);

            barName.Text = BarList.selectedBar;
            barEvaluation.Text = MainActivity.barInformation.Value[BarList.selectedBar + ""].ToString();
            newEvaluation.ProgressChanged += SeekBar_Changes;
            changeEvaluation.Click += delegate { MainActivity.barInformation.Value[BarList.selectedBar] = Int32.Parse(newEvaluationValue.Text); };
        }

        void SeekBar_Changes(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            newEvaluationValue.Text = string.Format("{0}", e.Progress);
        }
    }
}