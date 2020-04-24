﻿using System;
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
        ListView listOfAnimals;
        EditText textNameOfAnimal;
        Spinner animalChoice;
        AnimalAdapter adapter;

        List<string> animalsNameList = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            addSheepButton = FindViewById<Button>(Resource.Id.addSheepButton);
            textViewNumbSheep = FindViewById<TextView>(Resource.Id.textViewNumbSheep);
            listOfAnimals = FindViewById<ListView>(Resource.Id.listOfAnimals);
            textNameOfAnimal = FindViewById<EditText>(Resource.Id.textNameOfAnimal);
            animalChoice = FindViewById<Spinner>(Resource.Id.animalChoice);

            adapter = new AnimalAdapter(this);
            listOfAnimals.Adapter = adapter;

            addSheepButton.Click += AddSheepButton_Click;
            listOfAnimals.ItemClick += ListOfAnimals_ItemClick;
            animalChoice.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(AnimalChoice_ItemSelected);

            var adapterSpinner = ArrayAdapter.CreateFromResource(this, Resource.Array.type_animal, Android.Resource.Layout.SimpleSpinnerItem);
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
            StartActivity(intent);
        }

        private void AddSheepButton_Click(object sender, EventArgs e)
        {
            if (textNameOfAnimal.Text == "")
            {
                Toast.MakeText(this, "Укажите имя овцы", ToastLength.Short).Show();
            }
            else
            {
                if (animalsNameList.Contains(textNameOfAnimal.Text))
                {
                    Toast.MakeText(this, "Животное с таким именем уже существует. Измените имя", ToastLength.Short).Show();
                }
                else
                {
                    animalsNameList.Add(textNameOfAnimal.Text);
                    AddRandomAnimal();
                    textNameOfAnimal.Text = "";
                    count++;
                    textViewNumbSheep.Text = count.ToString();
                }
            }
        }

        public AnimalModel AddRandomAnimal()
        {
            AnimalModel animal;
            if(animalChoice.SelectedItemPosition == 0)
            {
                animal = new SheepModel();
            }
            else
            {
                animal = new WolfModel();
            }
            animal.Name = textNameOfAnimal.Text;
            adapter.Add(animal);
            adapter.NotifyDataSetChanged();
            return animal;
        }
    }
}