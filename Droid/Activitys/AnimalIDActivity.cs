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
        TextView _textSheepsName;
        ImageView _animalsFoto;
        TextView _animalType;
        TextView _whoKillMe;
        ImageView _imageAnimal;
        LinearLayout _starsLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AnimalIDLayout);
            _starsLayout = FindViewById<LinearLayout>(Resource.Id.starsLayout);
            _textSheepsName = FindViewById<TextView>(Resource.Id.textViewSheepsName);
            _animalsFoto = FindViewById<ImageView>(Resource.Id.animalsFoto);
            _animalType = FindViewById<TextView>(Resource.Id.animalType);
            _whoKillMe = FindViewById<TextView>(Resource.Id.whoKillMe);
            _imageAnimal = FindViewById<ImageView>(Resource.Id.imageAnimal);
            var typeOfAnimal = Intent.Extras.GetInt(Keys.TYPEofANIMAL);
            var animalID = Intent.Extras.GetString(Keys.ANIMAL_ID);
            var animal = businessLogic.GetAnimal(animalID, typeOfAnimal);
            var star = dataBase.GetID<Prey>(animalID);
            _textSheepsName.Text = animal.Name;

            KillersName(animal);
            _animalType.Text = businessLogic.TextKill(animal);
            var animalState = businessLogic.GetAnimalState(animal);
            _imageAnimal.SetImageResource(AnimalModelImager.GetAnimalImage(animal, animalState));
            StarPicture(star);
            AddBottomImage(animal);
        }

        public void StarPicture(int star)
        {
            for (int i = 0; i < star; i++)
            {
                var lPar = new TableLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent, 1);
                ImageView _imageStar = new ImageView(this);
                _imageStar.LayoutParameters = lPar;
                _starsLayout.AddView(_imageStar);
                _imageStar.SetImageResource(Resource.Drawable.star);
            }
        }

        public void KillersName(AnimalModel  animal)
        {
            var name = businessLogic.NameofKiller(animal);
            if (name != "")
            {
                _whoKillMe.Text = name;
                _whoKillMe.Visibility = ViewStates.Visible;
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
            load.Into(_animalsFoto);
        }
    }
}