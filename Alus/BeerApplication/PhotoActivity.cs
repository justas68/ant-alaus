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
using Android.Graphics;
using Android.Provider;

namespace BeerApplication
{
    [Activity(Label = "PhotoActivity")]
    public class PhotoActivity : Activity
    {
        private ImageView imageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_photo);
            // Create your application here
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            selectImage();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            if (requestCode == 0)
            {
                Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                imageView.SetImageBitmap(bitmap);
            }
            else
            {
                Android.Net.Uri uri = data.Data;
                imageView.SetImageURI(uri);
            }
        }

        private void selectImage()
        {
            System.String[] items = { "Take Photo", "Choose from Library", "Cancel" };

            using (var dialogBuilder = new AlertDialog.Builder(this))
            {
                dialogBuilder.SetTitle("Add Photo");
                dialogBuilder.SetItems(items, (d, args) =>
                {
                    //Take photo
                    if (args.Which == 0)
                    {
                        Intent intent = new Intent(MediaStore.ActionImageCapture);
                        StartActivityForResult(intent, 0);
                    }
                    //Choose from gallery
                    else if (args.Which == 1)
                    {
                        Intent intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
                        intent.SetType("image/*");
                        intent.SetAction(Intent.ActionGetContent);
                        StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 1);
                    }
                        else
                    {
                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                    }
                });
                dialogBuilder.Show();               
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