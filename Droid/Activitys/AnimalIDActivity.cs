using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Square.Picasso;


namespace Sheep_Wolf.Droid
{
    [Activity(Label = "AnimalID")]
    public class AnimalIDActivity : Activity
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
            var keys = new Keys();
            var animalName = Intent.Extras.GetString(keys.NAMEofANIMAL);
            var animalFoto = Intent.Extras.GetString(keys.FOTOofANIMAL);
            var typeOfAnimal = Intent.Extras.GetInt(keys.TYPEofANIMAL);
            var deadORalive = Intent.Extras.GetBoolean(keys.DEADofANIMAL);
            var killer = Intent.Extras.GetString(keys.KILLERofANIMAL, null);

            textViewSheepsName.Text = animalName;
            //animalType.Text = typeOfAnimal.ToString();
            Picasso.With(this)
                   .Load(animalFoto)
                   .Into(animalsFoto);

            if (typeOfAnimal == 0)
            {
                imageAnimal.SetImageResource(Resource.Drawable.sheep);
                animalType.Text = AnimalType.SHEEP.ToString();
            }
            if (typeOfAnimal == 1)
            {
                imageAnimal.SetImageResource(Resource.Drawable.wolf);
                animalType.Text = AnimalType.WOLF.ToString();
            }

            if (killer != null)
            {
                if (typeOfAnimal == 0)
                {
                    animalType.Text = $"This {AnimalType.SHEEP} eliminated by {killer}";
                }
                if (typeOfAnimal == 1)
                {
                    animalType.Text = $"This {AnimalType.WOLF} tear to pieces {killer}";
                }
            }

            if (deadORalive)
            {
                imageAnimal.SetImageResource(Resource.Drawable.rip);

                Picasso.With(this)
                       .Load(animalFoto)
                       .Transform(new GrayscaleTransformation())
                       .Into(animalsFoto);
            }
        }
    }
    enum AnimalType
    {
        SHEEP,
        WOLF
    }
}