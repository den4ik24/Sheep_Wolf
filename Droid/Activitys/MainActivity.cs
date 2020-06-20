using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Views.InputMethods;
using Android.Widget;
using Sheep_Wolf_NetStandardLibrary;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Views;
using Android.Text;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Support.V4.Content;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "Circle of Life", Icon = "@mipmap/icon", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView textViewNumbSheep;
        RecyclerView listOfAnimals;
        EditText textNameOfAnimal;
        Spinner animalChoice;
        AnimalAdapter adapter;
        V7Toolbar myToolbar;
        IMenu menu;
        IBusinessLogic businessLogic = new BusinessLogic();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            myToolbar = FindViewById<V7Toolbar>(Resource.Id.my_toolbar);
            textViewNumbSheep = FindViewById<TextView>(Resource.Id.textViewNumbSheep);
            listOfAnimals = FindViewById<RecyclerView>(Resource.Id.listOfAnimals);
            textNameOfAnimal = FindViewById<EditText>(Resource.Id.textNameOfAnimal);
            animalChoice = FindViewById<Spinner>(Resource.Id.animalChoice);
            
            SetSupportActionBar(myToolbar);
            var layoutManager = new LinearLayoutManager(this);
            var adapterSpinner = ArrayAdapter<string>.CreateFromResource(this, Resource.Array.type_animal, Android.Resource.Layout.SimpleSpinnerItem);
            adapter = new AnimalAdapter();
            
            listOfAnimals.SetLayoutManager(layoutManager);
            businessLogic.GetListAnimals();
            CountAnimal();
            adapter.animalModelsArray = businessLogic.AnimalModel();
            adapter.ItemClick += ListOfAnimals_ItemClick;
            listOfAnimals.SetAdapter(adapter);
            animalChoice.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(AnimalChoice_ItemSelected);
            adapterSpinner.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            animalChoice.Adapter = adapterSpinner;
            animalChoice.SetSelection(0);

            textNameOfAnimal.TextChanged += TextNameOfAnimal_TextChanged;
            
        }

        private void AnimalChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            animalChoice = sender as Spinner;
            string selectedAnimal = string.Format($"Выбрано животное - {animalChoice.GetItemAtPosition(e.Position)}");
            Toast.MakeText(this, selectedAnimal, ToastLength.Short).Show();
        }

        private void ListOfAnimals_ItemClick(object sender, int e)
        {
            var intent = new Intent(this, typeof(AnimalIDActivity));
            var animal = adapter.ElementPosition(e);
            DataTransmission(animal, intent);
            StartActivity(intent);
        }

        public void DataTransmission(AnimalModel animal, Intent intent)
        {
            AnimalType type;
            if (animal is SheepModel)
            {
                type = AnimalType.SHEEP;
            }
            else if (animal is DuckModel)
            {
                type = AnimalType.DUCK;
            }
            else
            {
                type = AnimalType.WOLF;
            }
            intent.PutExtra(Keys.TYPEofANIMAL, (int)type);
            intent.PutExtra(Keys.ANIMAL_ID, animal.Id);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            var item = menu.FindItem(Resource.Id.addAnimals);
            Drawable resIcon = ContextCompat.GetDrawable(this, Resource.Drawable.animal_logo);
 
            resIcon.Mutate().SetColorFilter(Color.DarkGray, PorterDuff.Mode.SrcIn);
            item.SetIcon(resIcon);
            item.SetEnabled(false);
            this.menu = menu;
            return true;
        }

        //public override bool OnPrepareOptionsMenu(IMenu menu)
        //{
        //}
        private void TextNameOfAnimal_TextChanged(object sender, TextChangedEventArgs e)
        {
            Drawable resIcon = ContextCompat.GetDrawable(this, Resource.Drawable.animal_logo);

            var item = menu.FindItem(Resource.Id.addAnimals);

            if (textNameOfAnimal.Text == "")
            {
                resIcon.Mutate().SetColorFilter(Color.DarkGray, PorterDuff.Mode.SrcIn);
                item.SetIcon(resIcon);
                item.SetEnabled(false);
            }
            else
            {
                item.SetEnabled(true);
                item.SetIcon(resIcon);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        { 
            switch (item.ItemId)
            {
                case Resource.Id.addAnimals:
                    if (textNameOfAnimal.Text == "")
                    {
                        //item.SetEnabled(false);
                        //item.Icon.SetAlpha(130);

                        Toast.MakeText(this, "Укажите имя существа", ToastLength.Short).Show();
                    }
                    else
                    {
                        //item.SetEnabled(true);
                        //item.Icon.SetAlpha(255);
                        AddRandomAnimal();
                        textNameOfAnimal.Text = "";
                    }
                    return true;

                case Resource.Id.addDucks:
                    businessLogic.AddDucks();
                    CountAnimal();
                    adapter.NotifyDataSetChanged();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void AddRandomAnimal()
        {
            var isSheep = animalChoice.SelectedItemPosition == 0;

            if(businessLogic.AddAnimal(isSheep, textNameOfAnimal.Text))
            {
                Toast.MakeText(this, "Животное с таким именем уже существует. Измените имя", ToastLength.Short).Show();
            }
            else
            {
                CountAnimal();
                adapter.NotifyDataSetChanged();
                //удаление клавиатуры
                InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(textNameOfAnimal.WindowToken, 0);
            }
        }

        public void CountAnimal()
        {
            textViewNumbSheep.Text = businessLogic.AnimalModel().Count.ToString();
        }
    }
}