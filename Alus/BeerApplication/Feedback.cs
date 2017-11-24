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
    [Activity(Label = "Feedback", Icon = "@drawable/BeerGlass")]
    class Feedback : Activity
    {
        EditText userEmail;
        EditText userFeedback;
        Button sendButton;
        Spinner feedbackOptions;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FeedbackLayout);

            userEmail = FindViewById<EditText>(Resource.Id.userEmail);
            userFeedback = FindViewById<EditText>(Resource.Id.userFeedback);
            sendButton = FindViewById<Button>(Resource.Id.sendButton);
            feedbackOptions = FindViewById<Spinner>(Resource.Id.feedbackOptions);

            feedbackOptions.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.feedback_options, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            feedbackOptions.Adapter = adapter;

            sendButton.Click += delegate
            {
                var email = new Intent(Android.Content.Intent.ActionSend);
                email.PutExtra(Android.Content.Intent.ExtraEmail, new string[] { userEmail.Text });
                email.PutExtra(Android.Content.Intent.ExtraSubject, feedbackOptions.SelectedItem.ToString());
                email.PutExtra(Android.Content.Intent.ExtraText, userFeedback.Text);
                email.SetType("message/rfc822");
                StartActivity(email);
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}