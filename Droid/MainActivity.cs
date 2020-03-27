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

        Random random = new Random();
        SheepAdapter adapter;

        List<string> sheepsNameList = new List<string>();
        string[] sheepsStringURL =
        {
            "https://www.studentofthegun.com/wp-content/uploads/2017/10/SOTG_679_-_A_Nation_of_Sheep.jpg",
            "http://risovach.ru/upload/2014/04/generator/naivnaya-ovechka2_48043820_orig_.jpeg",
            "https://www.parcs-zoologiques-lumigny.fr/wp-content/uploads/2019/03/mouton-1240.jpg",
            "https://yesofcorsa.com/wp-content/uploads/2017/04/4K-Sheep-Wallpaper-Gallery.jpg",
            "https://steemitimages.com/DQmY5jSVRybkLgwyoD8apfXDUsm3Baj2ryahwUCwuVrcS7j/lamb-2146961_1920.jpg",
            "https://www.tokkoro.com/picsup/3313963-sheep-lamb-wool-mother.jpg",
            "https://www.kidsbooksandpuppets.com/assets/images/folkmanislongwoolsheeppuppets2982slg.jpg",
            "https://www.focus.pl/uploads/media/default/0001/24/dolly.jpeg",
            "https://pix.avax.news/avaxnews/69/5c/00015c69.jpeg",
            "http://milifamily.pl/wp-content/uploads/2016/07/Untitled-design-18.jpg"
        };

        string[] wolfStringURL =
        {
            "https://www.proza.ru/pics/2014/03/17/1922.jpg",
            "https://imgfon.ru/Images/Download/Crop/2560x1600/Animals/volk-hischnik-vzglyad-sherst-lejit.jpg",
            "https://img2.goodfon.ru/original/1600x1200/e/27/les-volk-sneg.jpg",
            "https://wallpaperbro.com/img/256362.jpg",
            "https://i.artfile.me/wallpaper/07-09-2017/1920x1280/zhivotnye-volki--kojoty--shakaly-vzglyad-1224870.jpg",
            "https://s00.yaplakal.com/pics/pics_original/4/0/0/13729004.jpg",
            "https://i.ytimg.com/vi/GKK-nxCjSWc/maxresdefault.jpg",
            "https://image.wallperz.com/wp-content/uploads/2017/09/26/wallperz.com-20170926100049.jpg",
            "https://www.wallpaperup.com/uploads/wallpapers/2015/05/28/702184/fad311d0532eb1d00d28a093bd4abf8d-1400.jpg",
            "https://www.3d-hdwallpaper.com/wp-content/uploads/2019/05/desktop-free-wolf-wallpaper-download.jpg"
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

            animalChoice = FindViewById<Spinner>(Resource.Id.animalChoice);

            adapter = new SheepAdapter(this);
            listOfSheeps.Adapter = adapter;

            //animalChoice.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(AnimalChoice_ItemSelected);
            
        }

        //private void AnimalChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.type_animal, Android.Resource.Layout.SimpleSpinnerItem);
        //    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
        //    animalChoice.Adapter = adapter;

        //    Spinner spinner = (Spinner)sender;

        //    string selectedAnimal = string.Format($"Выбрано животное - {spinner.GetItemAtPosition(e.Position)}");
        //    Toast.MakeText(this, selectedAnimal, ToastLength.Short).Show();

        //    if(spinner.SelectedItemPosition == 0)
        //    {

        //    }

        //    if (spinner.SelectedItemPosition == 1)
        //    {

        //    }
        //}

        private void ListOfSheeps_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(SheepID));

            //intent.PutExtra("NAMEofSHEEP", listOfSheeps.GetItemAtPosition(e.Position).ToString());
            var N = adapter.ElementPosition(e.Position);
            
            intent.PutExtra("NAMEofSHEEP", N.Name);
            intent.PutExtra("FOTOofSHEEP", N.URL);
            StartActivity(intent);

        }

        private void AddSheepButton_Click(object sender, EventArgs e)
        {
            string temp = textNameOfSheep.Text;

            if (temp == "")
            {
                Toast.MakeText(this, "Укажите имя овцы", ToastLength.Short).Show();
            }

            else
            {
                //string temp = textNameOfSheep.Text;//[random.Next(sheepsNameList.Count)].ToString();
                //sheep.Name = textNameOfSheep.Text;

                if (sheepsNameList.Contains(temp))
                {
                    Toast.MakeText(this, "Животное с таким именем уже существует. Измените имя", ToastLength.Short).Show();
                }
                else
                {
                    var sheep = new SheepClass();

                    sheepsNameList.Add(temp);
                    sheep.Name = temp;
                    sheep.URL = Rand();


                    adapter.Add(sheep);
                    adapter.NotifyDataSetChanged();
                    textNameOfSheep.Text = "";
                    count++;
                    textViewNumbSheep.Text = count.ToString();
                }
            }
        }

        public string Rand()
        {
            return sheepsStringURL[random.Next(sheepsStringURL.Length)];
        }

    //    public string NameAr()
    //    {
    //            string temp = textNameOfSheep.Text[random.Next(0, sheepsNameList.Count)].ToString();

    //        if (sheepsNameList.Contains(temp))
    //        {
    //            Toast.MakeText(this, "Животное с таким именем уже существует. Измените имя", ToastLength.Short).Show();
    //        }
    //        else sheepsNameList.Add(temp);

    //        return temp;
    //    }
    }
}

