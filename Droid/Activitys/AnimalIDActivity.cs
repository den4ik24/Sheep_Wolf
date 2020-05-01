using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Square.Picasso;
using Sheep_Wolf_NetStandardLibrary;

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
            
            var animalName = Intent.Extras.GetString(Keys.NAMEofANIMAL);
            var animalFoto = Intent.Extras.GetString(Keys.FOTOofANIMAL);
            var typeOfAnimal = Intent.Extras.GetInt(Keys.TYPEofANIMAL);
            var deadORalive = Intent.Extras.GetBoolean(Keys.DEADofANIMAL);
            var killer = Intent.Extras.GetString(Keys.KILLERofANIMAL, null);

            textViewSheepsName.Text = animalName;

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
                    imageAnimal.SetImageResource(Resource.Drawable.killer);
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
}