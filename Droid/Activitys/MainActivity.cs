using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "Овцы/Волки", Icon = "@mipmap/icon", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button addSheepButton;
        TextView textViewNumbSheep;
        ListView listOfAnimals;
        EditText textNameOfAnimal;
        Spinner animalChoice;
        AnimalAdapter adapter;
        IBusinessLogic businessLogic = new BusinessLogic();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            addSheepButton = FindViewById<Button>(Resource.Id.addSheepButton);
            textViewNumbSheep = FindViewById<TextView>(Resource.Id.textViewNumbSheep);
            listOfAnimals = FindViewById<ListView>(Resource.Id.listOfAnimals);
            textNameOfAnimal = FindViewById<EditText>(Resource.Id.textNameOfAnimal);
            animalChoice = FindViewById<Spinner>(Resource.Id.animalChoice);

            businessLogic.GetAnimals();
            CountAnimal();
            adapter = new AnimalAdapter(this);
            adapter.animalModelsArray = businessLogic.AnimalModel();
            listOfAnimals.Adapter = adapter;

            addSheepButton.Click += AddSheepButton_Click;
            listOfAnimals.ItemClick += ListOfAnimals_ItemClick;
            animalChoice.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(AnimalChoice_ItemSelected);

            var adapterSpinner = ArrayAdapter<string>.CreateFromResource(this, Resource.Array.type_animal, Android.Resource.Layout.SimpleSpinnerItem);
            adapterSpinner.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            animalChoice.Adapter = adapterSpinner;
            animalChoice.SetSelection(0);
        }

        private void AnimalChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            animalChoice = sender as Spinner;
            string selectedAnimal = string.Format($"Выбрано животное - {animalChoice.GetItemAtPosition(e.Position)}");
            Toast.MakeText(this, selectedAnimal, ToastLength.Short).Show();
        }

        private void ListOfAnimals_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(AnimalIDActivity));
            var N = adapter.ElementPosition(e.Position);
            DataTransmission(N, intent);
            StartActivity(intent);
        }

        private void AddSheepButton_Click(object sender, EventArgs e)
        {
            if (textNameOfAnimal.Text == "")
            {
                Toast.MakeText(this, "Укажите имя существа", ToastLength.Short).Show();
            }
            else
            {
                AddRandomAnimal();
                textNameOfAnimal.Text = "";

            }
        }

        public void DataTransmission(AnimalModel N, Intent intent)
        {
            AnimalType type;
            if (N is SheepModel)
            {
                type = AnimalType.SHEEP;
            }
            else
            {
                type = AnimalType.WOLF;
            }

            intent.PutExtra(Keys.NAMEofANIMAL, N.Name);
            intent.PutExtra(Keys.FOTOofANIMAL, N.URL);
            intent.PutExtra(Keys.TYPEofANIMAL, (int)type);
            if (N.IsDead)
            {
                intent.PutExtra(Keys.DEADofANIMAL, N.IsDead);
            }
            if (!string.IsNullOrEmpty(N.Killer))
            {
                intent.PutExtra(Keys.KILLERofANIMAL, N.Killer);
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