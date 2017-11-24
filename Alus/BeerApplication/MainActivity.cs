using Android.App;
using Android.Widget;
using Android.OS;
using Java.Lang;
using Android.Views;
using Android.Content;
using System;
using Android.Runtime;
using System.Collections.Generic;

namespace BeerApplication
{
    [Activity(Label = "Beer Application", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public static Lazy<Dictionary<string, int>> barInformation = new Lazy<Dictionary<string, int>>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main_page);
            var statisticButton = FindViewById<ImageButton>(Resource.Id.StatisticButton);
            var evaluationButton = FindViewById<ImageButton>(Resource.Id.EvaluationButton);
            var beerGlassButton = FindViewById<ImageButton>(Resource.Id.BeerGlassButton);
            var feedbackButton = FindViewById<ImageButton>(Resource.Id.FeedbackButton);
            var nearestBarButton = FindViewById<ImageButton>(Resource.Id.NearestBarButton);

            beerGlassButton.Click += (sender, e) =>
            {
                Intent next1 = new Intent(this, typeof(PhotoActivity));
                StartActivity(next1);
            };

            statisticButton.Click += delegate
            {
                Intent next2 = new Intent(this, typeof(StatisticTable));
                StartActivity(next2);
            };

            feedbackButton.Click += delegate
            {
                Intent next3 = new Intent(this, typeof(Feedback));
                StartActivity(next3);
            };
            nearestBarButton.Click += delegate
            {
                Intent next4 = new Intent(this, typeof(NearestBars));
                StartActivity(next4);
            };
            evaluationButton.Click += delegate
            {
                Intent next5 = new Intent(this, typeof(BarList));
                StartActivity(next5);
            };
        }
    }
}
