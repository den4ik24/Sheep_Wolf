using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Square.Picasso;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "SheepID")]
    public class SheepID : Activity
    {
        TextView textViewSheepsName;
        ImageView sheepFoto;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SheepIDLayout);

            textViewSheepsName = FindViewById<TextView>(Resource.Id.textViewSheepsName);
            sheepFoto = FindViewById<ImageView>(Resource.Id.sheepFoto);

            var sheepName = Intent.Extras.GetString("NAMEofSHEEP");
            textViewSheepsName.Text = sheepName;

            var fotoSheep = Intent.Extras.GetString("FOTOofSHEEP");
            Picasso.With(this)
                .Load(fotoSheep)
                .Into(sheepFoto);

        }
    }
}