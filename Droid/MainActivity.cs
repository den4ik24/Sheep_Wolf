using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "Овцы/Волки", Icon = "@mipmap/icon", MainLauncher = true)]
    public class MainActivity : Activity
    {
        int count = 0;

        Button addSheepButton;
        TextView textViewNumbSheep;
        ListView listOfSheeps;
        EditText textNameOfSheep;
        Spinner animalChoice;
        TextView animalName;
        AnimalAdapter adapter;

        List<string> animalsNameList = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);

            textViewNumbSheep = FindViewById<TextView>(Resource.Id.textViewNumbSheep);
            listOfSheeps = FindViewById<ListView>(Resource.Id.listOfSheeps);
            listOfSheeps.ItemClick += ListOfSheeps_ItemClick;

            addSheepButton = FindViewById<Button>(Resource.Id.addSheepButton);
            addSheepButton.Click += AddSheepButton_Click;

            textNameOfSheep = FindViewById<EditText>(Resource.Id.textNameOfSheep);

            animalChoice = FindViewById<Spinner>(Resource.Id.animalChoice);

            adapter = new AnimalAdapter(this);
            listOfSheeps.Adapter = adapter;

            animalChoice.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(AnimalChoice_ItemSelected);
            var adapterSpinner = ArrayAdapter.CreateFromResource(this, Resource.Array.type_animal, Android.Resource.Layout.SimpleSpinnerItem);
            adapterSpinner.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            animalChoice.Adapter = adapterSpinner;
            animalChoice.SetSelection(0);

            animalName = FindViewById<TextView>(Resource.Id.textViewSheepAdapter);
     
        }

        private void AnimalChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            animalChoice = sender as Spinner;

            string selectedAnimal = string.Format($"Выбрано животное - {animalChoice.GetItemAtPosition(e.Position)}");
            Toast.MakeText(this, selectedAnimal, ToastLength.Short).Show();

      
        }

        private void ListOfSheeps_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(SheepID));

            //intent.PutExtra("NAMEofSHEEP", listOfSheeps.GetItemAtPosition(e.Position).ToString());
            var N = adapter.ElementPosition(e.Position);
            
            intent.PutExtra("NAMEofSHEEP", N.Name);
            intent.PutExtra("FOTOofSHEEP", N.URL);
            intent.PutExtra("TYPEofSHEEP", N.Type);
            StartActivity(intent);


        }

        private void AddSheepButton_Click(object sender, EventArgs e)
        {

            if (textNameOfSheep.Text == "")
            {
                Toast.MakeText(this, "Укажите имя овцы", ToastLength.Short).Show();
            }

            else
            {

                if (animalsNameList.Contains(textNameOfSheep.Text))
                {
                    Toast.MakeText(this, "Животное с таким именем уже существует. Измените имя", ToastLength.Short).Show();
                }
                else
                {

                    animalsNameList.Add(textNameOfSheep.Text);
                    RandAnimal();

                    textNameOfSheep.Text = "";
                    count++;
                    textViewNumbSheep.Text = count.ToString();
                }
            }
        }

        public AnimalClass RandAnimal()
        {
            AnimalClass animal;
            if(animalChoice.SelectedItemPosition == 0)
            {
                animal = new SheepClass();
            }
            else
            {
                animal = new WolfClass();
            }
            animal.Name = textNameOfSheep.Text;
            adapter.Add(animal);
            adapter.NotifyDataSetChanged();
            return animal;
        }
    }
}

