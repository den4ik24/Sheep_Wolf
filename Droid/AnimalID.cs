using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Square.Picasso;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "AnimalID")]
    public class AnimalID : Activity
    {

        TextView textViewSheepsName;
        ImageView animalsFoto;
        TextView animalType;
        ImageView imageAnimal;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AnimalIDLayout);

            textViewSheepsName = FindViewById<TextView>(Resource.Id.textViewSheepsName);
            animalsFoto = FindViewById<ImageView>(Resource.Id.animalsFoto);
            animalType = FindViewById<TextView>(Resource.Id.animalType);
            imageAnimal = FindViewById<ImageView>(Resource.Id.imageAnimal);

            var animalName = Intent.Extras.GetString("NAMEofANIMAL");
            textViewSheepsName.Text = animalName;

            var animalFoto = Intent.Extras.GetString("FOTOofANIMAL");
            Picasso.With(this)
                .Load(animalFoto)
                .Into(animalsFoto);

            var typeOfAnimal = Intent.Extras.GetString("TYPEofANIMAL");
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