using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "SheepID")]
    public class SheepID : Activity
    {
        TextView textViewSheepsName;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SheepIDLayout);

            textViewSheepsName = FindViewById<TextView>(Resource.Id.textViewSheepsName);

            var name = Intent.Extras.GetString("NAMEofSHEEP");
            textViewSheepsName.Text = name.ToString();

            var sheepURL = Intent.GetStringExtra("SheepsURL");

        }
    }
}