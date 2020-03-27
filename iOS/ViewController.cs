using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 0;
        Random random = new Random();
        List<SheepClassIOS> sheepNamesArray = new List<SheepClassIOS>();
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

        string Rand;
        AnimalPickerModel picker;
        List<string> Animals = new List<string>
        {
            "SHEEP", "WOLF"
        };

        public ViewController(IntPtr handle) : base(handle)
        {
           
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            ButtonAddSheep.TouchUpInside += ButtonAddSheep_TouchUpInside;

            picker = new AnimalPickerModel(Animals);
            animalChoice.Model = picker;

            //picker.ValueChanged += Picker_ValueChanged;
            picker.ValueChanged += (sender, e) =>
            {
              
                if (picker.SelectedValue == "SHEEP")
                {
                    Rand = RandSheep();
                }

                if (picker.SelectedValue == "WOLF")
                {
                    Rand = RandWolf();
                }
            };
        }

        //private void Picker_ValueChanged(object sender, EventArgs e)
        //{
          
        //}

        private void ButtonAddSheep_TouchUpInside(object sender, EventArgs e)
        {
            string temp = textNameOfSheep.Text;

            if (temp == "")
            {

                var alertController = UIAlertController.Create
                    ("WARNING", "Введите имя овцы", UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create
                    ("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alertController, true, null);

            }
            else
            {
                if (sheepsNameList.Contains(temp))
                {
                    var alertController = UIAlertController.Create
                    ("WARNING", "Животное с таким именем уже существует.Измените имя", UIAlertControllerStyle.Alert);
                    alertController.AddAction(UIAlertAction.Create
                        ("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(alertController, true, null);
                }

                else
                {
                    var sheep = new SheepClassIOS();

                    

                    sheepsNameList.Add(temp);
                    sheep.Name = temp;
                    sheep.URL = Rand;
                    sheep.Type = picker.SelectedValue;

                    sheepNamesArray.Add(sheep);
                    listOfSheeps.Source = new TableSource(sheepNamesArray, this);
                    listOfSheeps.ReloadData();
                    count++;
                    LabelNumberSheep.Text = count.ToString();
                    textNameOfSheep.Text = "";
                }
            }
        }

        public string RandWolf()
        {
            return wolfStringURL[random.Next(0, 10)];
        }

        public string RandSheep()
        {
            return sheepsStringURL[random.Next(0, 10)];
        }
    }
}
