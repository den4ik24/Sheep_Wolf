using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Sheep_Wolf_NetStandardLibrary;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

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
        readonly IBusinessLogic businessLogic = new BusinessLogic();
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
            var type = businessLogic.TypeOfAnimal(animal);
            intent.PutExtra(Keys.TYPEofANIMAL, (int)type);
            intent.PutExtra(Keys.ANIMAL_ID, animal.Id);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            var resIcon = ContextCompat.GetDrawable(this, Resource.Drawable.animal_logo);
            var item = menu.FindItem(Resource.Id.addAnimals);
            SetIconColorDisabled(item, resIcon);
            this.menu = menu;
            return true;
        }

        private void TextNameOfAnimal_TextChanged(object sender, TextChangedEventArgs e)
        {
            var resIcon = ContextCompat.GetDrawable(this, Resource.Drawable.animal_logo);
            var item = menu.FindItem(Resource.Id.addAnimals);
            if (textNameOfAnimal.Text == "")
            {
                SetIconColorDisabled(item, resIcon);
            }
            else
            {
                SetEnabledIconState(item, resIcon, true);
            }
        }

        public void SetEnabledIconState(IMenuItem item, Drawable resIcon, bool enabled)
        {
            item.SetIcon(resIcon);
            item.SetEnabled(enabled);
        }
        
        public void SetIconColorDisabled(IMenuItem item, Drawable resIcon)
        {
            resIcon.Mutate().SetColorFilter(Color.DarkGray, PorterDuff.Mode.SrcIn);
            SetEnabledIconState(item, resIcon, false);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        { 
            switch (item.ItemId)
            {
                case Resource.Id.addAnimals:
                    if (textNameOfAnimal.Text == "")
                    {
                        Toast.MakeText(this, "Укажите имя существа", ToastLength.Short).Show();
                    }
                    else
                    {
                        AddRandomAnimal();
                        textNameOfAnimal.Text = "";
                    }
                    return true;

                //case Resource.Id.addDucks:
                //    businessLogic.AddDucks();
                //    CountAnimal();
                //    adapter.NotifyDataSetChanged();
                //    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void AddRandomAnimal()
        {
            var isSheep = animalChoice.SelectedItemPosition;

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
    ///
}