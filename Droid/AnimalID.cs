using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.Animations;
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

            var deadORalive = Intent.Extras.GetBoolean("DEADofANIMAL");

            if (deadORalive)
            {
                imageAnimal.SetImageResource(Resource.Drawable.rip);
                animalType.Text = $"This {typeOfAnimal} eliminated by wolf";

                Picasso.With(this)
                       .Load(animalFoto)
                       .Transform(new GrayscaleTransformation())
                       .Into(animalsFoto);
            }
            else
            {
                if (typeOfAnimal == "WOLF")
                {
                    imageAnimal.SetImageResource(Resource.Drawable.wolf);
                }
                if (typeOfAnimal == "SHEEP")
                {
                    imageAnimal.SetImageResource(Resource.Drawable.sheep);
                }
            }
        }
    }
}