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

        Random random = new Random();
        string[] sheepsStringURL =
        {
            "https://www.studentofthegun.com/wp-content/uploads/2017/10/SOTG_679_-_A_Nation_of_Sheep.jpg",
            "https://avatars.mds.yandex.net/get-pdb/2073435/ec723bf1-4895-4823-b2f2-e696f224d2d0/s1200?webp=false",
            "https://www.parcs-zoologiques-lumigny.fr/wp-content/uploads/2019/03/mouton-1240.jpg",
            "https://pbs.twimg.com/media/Dx9wFWSWoAAVEbr.jpg",
            "https://steemitimages.com/DQmY5jSVRybkLgwyoD8apfXDUsm3Baj2ryahwUCwuVrcS7j/lamb-2146961_1920.jpg",
            "https://www.tokkoro.com/picsup/3313963-sheep-lamb-wool-mother.jpg",
            "https://www.kidsbooksandpuppets.com/assets/images/folkmanislongwoolsheeppuppets2982slg.jpg",
            "https://www.focus.pl/uploads/media/default/0001/24/dolly.jpeg",
            "https://wall.cookdiary.net/sites/default/files/wallpaper/animal/66269/sheep-wallpapers-hd-66269-4788701.png",
            "http://milifamily.pl/wp-content/uploads/2016/07/Untitled-design-18.jpg"
        };

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
            intent.PutExtra("NAMEofSHEEP", listOfSheeps.GetItemAtPosition(e.Position).ToString());
            //intent.PutExtra("SheepsURL",sheepsURL);
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
                var sheep = new SheepClass();

                sheep.Name = textNameOfSheep.Text;
                sheep.URL = sheepsStringURL[random.Next(0, 10)];


                adapter.Add(sheep);
                adapter.NotifyDataSetChanged();
                textNameOfSheep.Text = "";
                count++;
                textViewNumbSheep.Text = count.ToString();
            }

        }


    }
}

