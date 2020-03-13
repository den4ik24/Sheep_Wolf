using System;
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

        SheepAdapter adapter;

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

            adapter = new SheepAdapter(this);
            
            listOfSheeps.Adapter = adapter;
        }

        private void ListOfSheeps_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(SheepID));
            intent.PutExtra("NAMEofSHEEP", e.Position);
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
                adapter.Add(textNameOfSheep.Text);
                adapter.NotifyDataSetChanged();
                textNameOfSheep.Text = "";
                count++;
                textViewNumbSheep.Text = count.ToString();
            }

        }


    }
}

