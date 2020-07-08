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
        IBusinessLogic businessLogic = new BusinessLogic();
        TextView textSheepsName;
        ImageView animalsFoto;
        TextView animalType;
        ImageView imageAnimal;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AnimalIDLayout);
            textSheepsName = FindViewById<TextView>(Resource.Id.textViewSheepsName);
            animalsFoto = FindViewById<ImageView>(Resource.Id.animalsFoto);
            animalType = FindViewById<TextView>(Resource.Id.animalType);
            imageAnimal = FindViewById<ImageView>(Resource.Id.imageAnimal);

            var typeOfAnimal = Intent.Extras.GetInt(Keys.TYPEofANIMAL);
            var animalID = Intent.Extras.GetInt(Keys.ANIMAL_ID);
            var animal = businessLogic.GetAnimal(animalID, typeOfAnimal);

            textSheepsName.Text = animal.Name;
            Picasso.Get()
                   .Load(animal.URL)
                   .Into(animalsFoto);

            if (animal is SheepModel)
            {
                imageAnimal.SetImageResource(Resource.Drawable.sheep);
                animalType.Text = AnimalType.SHEEP.ToString();
            }
            if (animal is DuckModel)
            {
                imageAnimal.SetImageResource(Resource.Drawable.duck);
                animalType.Text = AnimalType.DUCK.ToString();
            }
            if (animal is WolfModel)
            {
                imageAnimal.SetImageResource(Resource.Drawable.wolf);
                animalType.Text = AnimalType.WOLF.ToString();
            }
            if(animal is HunterModel)
            {
                imageAnimal.SetImageResource(Resource.Drawable.hunter);
                animalType.Text = AnimalType.HUNTER.ToString();
            }

            if (animal.Killer != null)
            {
                if (animal is WolfModel)
                {
                    animalType.Text = $"This {AnimalType.WOLF} tear to pieces {animal.Killer}";
                    imageAnimal.SetImageResource(Resource.Drawable.killer);
                }
            }
            if (animal.WhoKilledMe != null)
            {
                if (animal is SheepModel)
                {
                    animalType.Text = $"This {AnimalType.SHEEP} eliminated by {animal.WhoKilledMe}";
                }
            }

            if (animal.IsDead)
            {
                imageAnimal.SetImageResource(Resource.Drawable.rip);

                Picasso.Get()
                       .Load(animal.URL)
                       .Transform(new GrayscaleTransformation())
                       .Into(animalsFoto);
            }
        }
    }
}