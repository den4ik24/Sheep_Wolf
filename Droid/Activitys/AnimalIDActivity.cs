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
            var animalID = Intent.Extras.GetString(Keys.ANIMAL_ID);
            var animal = businessLogic.GetAnimal(animalID, typeOfAnimal);
            var star = dataBase.GetID<Prey>(animalID);
            textSheepsName.Text = animal.Name;

            animalType.Text = businessLogic.TextKill(animal);
            whoKillMe.Text = businessLogic.NameofKiller(animal);
            var animalState = businessLogic.GetAnimalState(animal);
            imageAnimal.SetImageResource(AnimalModelImager.GetAnimalImage(animal, animalState));
            StarPicture(star);
            AddBottomImage(animal);
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

    public static class AnimalModelImager
    {
        public static int GetAnimalImage(AnimalModel model, AnimalState state)
        {
            if (model is SheepModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Resource.Drawable.sheep;
                }
                if(state == AnimalState.DEAD)
                {
                    return Resource.Drawable.rip;
                }
            }

            if (model is WolfModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Resource.Drawable.wolf;
                }
                if (state == AnimalState.DEAD)
                {
                    return Resource.Drawable.wolf_rip;
                }
                if (state == AnimalState.KILLER)
                {
                    return Resource.Drawable.killer;
                }
            }

            if (model is HunterModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Resource.Drawable.hunter;
                }
                if (state == AnimalState.DEAD)
                {
                    return Resource.Drawable.hunter_rip;
                }
                if (state == AnimalState.KILLER)
                {
                    return Resource.Drawable.hunter_killer;
                }
            }

            if(model is DuckModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Resource.Drawable.duck;
                }
            }
            throw new Exception();
        }
    }
}