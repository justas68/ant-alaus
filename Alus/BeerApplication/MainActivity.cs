using Android.App;
using Android.Widget;
using Android.OS;
using Java.Lang;
using Android.Views;
using Android.Content;
using System;
using Android.Runtime;

namespace BeerApplication
{
    [Activity(Label = "Beer Application", MainLauncher = true)]
    public class MainActivity : Activity
    {
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
                Intent next = new Intent(this, typeof(PhotoActivity));
                StartActivity(next);
            };

            statisticButton.Click += delegate
            {
                Intent next = new Intent(this, typeof(StatisticTable));
                StartActivity(next);
            };

            feedbackButton.Click += delegate
            {
                Intent next = new Intent(this, typeof(Feedbacks));
                StartActivity(next);
            };
            nearestBarButton.Click += delegate
            {
                Intent next = new Intent(this, typeof(NearestBars));
                StartActivity(next);
            };
        }
    }
}