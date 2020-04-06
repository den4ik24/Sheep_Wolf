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
        TextView animalType;
        ImageView imageAnimal;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SheepIDLayout);

            textViewSheepsName = FindViewById<TextView>(Resource.Id.textViewSheepsName);
            sheepFoto = FindViewById<ImageView>(Resource.Id.sheepFoto);
            animalType = FindViewById<TextView>(Resource.Id.animalType);
            imageAnimal = FindViewById<ImageView>(Resource.Id.imageAnimal);

            var animalName = Intent.Extras.GetString("NAMEofSHEEP");
            textViewSheepsName.Text = animalName;

            var animalFoto = Intent.Extras.GetString("FOTOofSHEEP");
            Picasso.With(this)
                .Load(animalFoto)
                .Into(sheepFoto);

            var typeOfAnimal = Intent.Extras.GetString("TYPEofSHEEP");
            animalType.Text = typeOfAnimal;

            if (typeOfAnimal == "WOLF")
            {
                imageAnimal.SetImageResource(Resource.Drawable.wolf);
            }
            else
            {
                imageAnimal.SetImageResource(Resource.Drawable.sheep);
            }
        }
    }
}