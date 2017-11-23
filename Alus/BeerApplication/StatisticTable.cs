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
using static Android.App.ActionBar;
using Android.Graphics;

namespace BeerApplication
{
    [Activity(Label = "Statistic Table")]
    public class StatisticTable : Activity
    {
        private string color= "#a8cbcc";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_statistic_table);
            // Create your application here
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            TableLayout tableLayout = FindViewById<TableLayout>(Resource.Id.tableLayout1);
            for (int i = 0; i < 4; i++)
            {
                TableRow row = new TableRow(this);
                row.SetGravity(GravityFlags.Center);
                TextView tv1 = new TextView(this);
                tv1.Text = "Paparazzi bar";
                tv1.SetPadding(5,40,5,40);
                tv1.SetBackgroundColor(Color.ParseColor(color));
                tv1.Gravity = GravityFlags.Center;
                tv1.TextAlignment = TextAlignment.Center;
                tv1.SetTextColor(Color.ParseColor("#000000"));
                row.AddView(tv1);
                tv1.Click += delegate
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetMessage("Paparazzi bar\n" + "Bar address");
                    alert.SetPositiveButton("OK", (sender, args) =>
                    {
                        alert.Dispose();
                    });
                    alert.Show();
                };
                TextView tv2 = new TextView(this);
                tv2.Text = "4";
                tv2.SetPadding(5, 40, 5, 40);
                tv2.Gravity = GravityFlags.Center;
                tv2.SetBackgroundColor(Color.ParseColor(color));
                tv2.SetTextColor(Color.ParseColor("#000000"));
                row.AddView(tv2);
                TextView tv3 = new TextView(this);
                tv3.Text = "8";
                tv3.SetPadding(5, 40, 5, 40);
                tv3.Gravity = GravityFlags.Center;
                tv3.SetBackgroundColor(Color.ParseColor(color));
                tv3.SetTextColor(Color.ParseColor("#000000"));
                row.AddView(tv3);
                TextView tv4 = new TextView(this);
                tv4.Text = "80%";
                tv4.SetPadding(5, 40, 5, 40);
                tv4.Gravity = GravityFlags.Center;
                tv4.SetBackgroundColor(Color.ParseColor(color));
                tv4.SetTextColor(Color.ParseColor("#000000"));
                row.AddView(tv4);
                tableLayout.AddView(row);
                if(color == "#cdf7f9")
                {
                    color = "#a8cbcc";
                }
                else
                {
                    color = "#cdf7f9";
                }
            }

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}