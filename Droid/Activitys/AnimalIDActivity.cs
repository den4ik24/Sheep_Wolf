using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Square.Picasso;
using Sheep_Wolf_NetStandardLibrary;
using Android.Views;
using System;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "AnimalID")]
    public class AnimalIDActivity : Activity
    {
        IBusinessLogic businessLogic = new BusinessLogic();
        IDataBase dataBase = new DataBase();
        TextView textSheepsName;
        ImageView animalsFoto;
        TextView animalType;
        TextView whoKillMe;
        ImageView imageAnimal;
        LinearLayout starsLayout;

        //int GetImage(AnimalModel model)
        //{
        //    if(model is DuckModel)
        //    {
        //        return Resource.Drawable.duck;
        //    }

        //    throw new Exception();
        //}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AnimalIDLayout);
            

            starsLayout = FindViewById<LinearLayout>(Resource.Id.starsLayout);
            textSheepsName = FindViewById<TextView>(Resource.Id.textViewSheepsName);
            animalsFoto = FindViewById<ImageView>(Resource.Id.animalsFoto);
            animalType = FindViewById<TextView>(Resource.Id.animalType);
            whoKillMe = FindViewById<TextView>(Resource.Id.whoKillMe);
            imageAnimal = FindViewById<ImageView>(Resource.Id.imageAnimal);

            var typeOfAnimal = Intent.Extras.GetInt(Keys.TYPEofANIMAL);
            var animalID = Intent.Extras.GetInt(Keys.ANIMAL_ID);
            var animal = businessLogic.GetAnimal(animalID, typeOfAnimal);

            var star = dataBase.GetID<Prey>(animalID);

            textSheepsName.Text = animal.Name;

            //if (animal is SheepModel)
            //{
            //    imageAnimal.SetImageResource(Resource.Drawable.sheep);
            //    animalType.Text = AnimalType.SHEEP.ToString();
            //}
            //if (animal is DuckModel)
            //{
            //    imageAnimal.SetImageResource(Resource.Drawable.duck);
            //    animalType.Text = AnimalType.DUCK.ToString();
            //}
            //if (animal is WolfModel)
            //{
            //    imageAnimal.SetImageResource(Resource.Drawable.wolf);
            //    animalType.Text = AnimalType.WOLF.ToString();
            //}
            //if (animal is HunterModel)
            //{
            //    imageAnimal.SetImageResource(Resource.Drawable.hunter);
            //    animalType.Text = AnimalType.HUNTER.ToString();
            //}
            //imageAnimal.SetImageResource(GetImage(animal));
            animalType.Text = bl.getAnimalType(animal);
            var animalState = bl.getAnimalState(animal);
            imageAnimal.SetImageResource(AnimalModelImager.GetAnimalImage(animal, animalState));
            StarPicture(star);
            whoKillMe.Text = GetText(animal);
            AddBottomImage(animal);

            //if (animal.Killer != null)
            //{
            //    if (animal is WolfModel)
            //    {
            //        animalType.Text = $"This {AnimalType.WOLF} tear to pieces {animal.Killer}";
            //        //Toast.MakeText(this, $"Этот волк растерзал на клочки {animal.WhoKilledMe}", ToastLength.Short).Show();
            //        imageAnimal.SetImageResource(Resource.Drawable.killer);
            //        StarPicture(star);
            //    }
            //    if(animal is HunterModel)
            //    {
            //        animalType.Text = $"This {AnimalType.HUNTER} just kill a {animal.Killer}";
            //        //Toast.MakeText(this, $"Этот киллер только что завалил волка {animal.WhoKilledMe}", ToastLength.Short).Show();
            //        imageAnimal.SetImageResource(Resource.Drawable.hunter_killer);
            //        StarPicture(star);
            //    }
            //}
            //if (animal.WhoKilledMe != null)
            //{
            //    if (animal is SheepModel)
            //    {
            //        whoKillMe.Text = $"This {AnimalType.SHEEP} eliminated by {animal.WhoKilledMe}";
            //        Toast.MakeText(this, $"Эта овечка была растерзана на клочки волком {animal.WhoKilledMe}", ToastLength.Short).Show();
            //    }
            //    if (animal is WolfModel)
            //    {
            //        whoKillMe.Text = $"This {AnimalType.WOLF} is killed by a hunter {animal.WhoKilledMe}";
            //        Toast.MakeText(this, $"Этот волк завален доблестным охотником {animal.WhoKilledMe}", ToastLength.Short).Show();
            //    }
            //    if (animal is HunterModel)
            //    {
            //        whoKillMe.Text = $"This {AnimalType.HUNTER} is tear to pieces by a wolf {animal.WhoKilledMe}";
            //        Toast.MakeText(this, $"Этот охотник растерзан на клочки волком {animal.WhoKilledMe}", ToastLength.Short).Show();
            //    }
            //}



        }

        public void StarPicture(int star)
        {
            for (int i = 0; i < star; i++)
            {
                var lPar = new TableLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent, 1);
                ImageView imageStar = new ImageView(this);
                imageStar.LayoutParameters = lPar;
                starsLayout.AddView(imageStar);
                imageStar.SetImageResource(Resource.Drawable.star);
            }
        }

        public void AddBottomImage(AnimalModel animal)
        {
            var load = Picasso.Get()
                       .Load(animal.URL);
            if (animal.IsDead)
            {
                load.Transform(new GrayscaleTransformation());
            }
            load.Into(animalsFoto);

        }
}

static class AnimalModelImager
{
    int GetAnimalImage(AnimalModel model, AnimalState state)
    {
        if(model is SheepModel)
        {
            if (state.Alive)
            {
                return Resource.Drawable.sheep;
            }
        }
    }
}