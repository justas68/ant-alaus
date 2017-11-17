using Android.App;
using Android.Widget;
using Android.OS;
using Java.Lang;
using Android.Views;
using Android.Content;

namespace BeerApplication
{
    [Activity(Label = "Beer Application", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
 
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var button = FindViewById<ImageButton>(Resource.Id.StatisticButton);
            button.Click += delegate
            {
                Intent next = new Intent(this, typeof(StatisticTable));
                StartActivity(next);
            };
        }

        public void BeerglassOnClick(View view)
        {
            StartActivity(typeof(StatisticTable));
        }

        public void BarEvaluationOnClick(View view)
        {
            StartActivity(typeof(StatisticTable));
        }

        public void NearestBarsOnClick(View view)
        {
            StartActivity(typeof(StatisticTable));
        }

        public void FeedbackOnClick(View view)
        {
            StartActivity(typeof(StatisticTable));
        }

        public void StatisticalTableOnClick(View view)
        {
            Intent intent = new Intent(this, typeof(StatisticTable));
            StartActivity(intent);
        }
    }
}

