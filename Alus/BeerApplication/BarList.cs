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
    [Activity(Label = "List of bars", Icon = "@drawable/BeerGlass")]

    class BarList : Activity
    {
        public static string selectedBar;

        private ListView listView;

        private Button sortBtn;
        private Button addButton;
        private Button refresh;
        string[] barNames;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListOfBars);
            InitializeBarInformation();
            this.InitializeViews();

            Populate();
            sortBtn.Click += SortBtn_Click;
            addButton.Click += AddBtn_Click;
            refresh.Click += Refresh;
            listView.ItemClick += GetSelected;
        }

        private void InitializeViews()
        {
            listView = FindViewById<ListView>(Resource.Id.lv);
            sortBtn = FindViewById<Button>(Resource.Id.sortBtn);
            addButton = FindViewById<Button>(Resource.Id.addBtn);
            refresh = FindViewById<Button>(Resource.Id.refresh);
        }

        private void GetSelected(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            String selectedFromList = listView.GetItemAtPosition(e.Position).ToString();
            selectedBar = selectedFromList;
            Intent next = new Intent(this, typeof(SelectedBar));
            StartActivity(next);
        }

        private void InitializeBarInformation()
        {
            MainActivity.barInformation.Value.Add("Snekutis", 8);
            MainActivity.barInformation.Value.Add("Metruske", 10);
            MainActivity.barInformation.Value.Add("Mrs. Pub", 7);
            barNames = MainActivity.barInformation.Value.Keys.ToArray();
        }

        private void Populate()
        {
            listView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, barNames);
        }

        void AddBtn_Click(object sender, EventArgs e)
        {
            Intent next = new Intent(this, typeof(NewBar));
            StartActivity(next);
        }

        void SortBtn_Click(object sender, EventArgs e)
        {
            Sort();
        }

        void Refresh(object sender, EventArgs e)
        {
            barNames = MainActivity.barInformation.Value.Keys.ToArray();
            Populate();
        }

        private void Sort()
        {
            Lazy<Dictionary<string, int>> sortedBarInformation = new Lazy<Dictionary<string, int>>();
            var items = from pair in MainActivity.barInformation.Value orderby pair.Value descending select pair;

            foreach (KeyValuePair<string, int> pair in items)
            {
                sortedBarInformation.Value.Add(pair.Key.ToString(), (int)pair.Value);
            }
            MainActivity.barInformation = sortedBarInformation;
            barNames = MainActivity.barInformation.Value.Keys.ToArray();
            Populate();
        }
    }
}
