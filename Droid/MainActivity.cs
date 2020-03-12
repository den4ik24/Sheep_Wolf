using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "Sheep_Wolf", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 0;

        Button addSheepButton;
        TextView textViewNumbSheep;
        ListView listOfSheeps;
        EditText textNameOfSheep;

        SheepAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            textViewNumbSheep = FindViewById<TextView>(Resource.Id.textViewNumbSheep);
            listOfSheeps = FindViewById<ListView>(Resource.Id.listOfSheeps);

            addSheepButton = FindViewById<Button>(Resource.Id.addSheepButton);
            addSheepButton.Click += AddSheepButton_Click;

            textNameOfSheep = FindViewById<EditText>(Resource.Id.textNameOfSheep);

            adapter = new SheepAdapter(this);
            
            listOfSheeps.Adapter = adapter;
        }

        private void AddSheepButton_Click(object sender, EventArgs e)
        {

            if (textNameOfSheep.Text == "")
            {
                Toast.MakeText(this, "Укажите имя овцы", ToastLength.Short).Show();
            }

            else
            {
                adapter.Add(textNameOfSheep.Text);
                adapter.NotifyDataSetChanged();
                textNameOfSheep.Text = "";
                count++;
                textViewNumbSheep.Text = count.ToString();
            }

        }
    }
}

