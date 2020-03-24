using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 0;
        List<SheepClassIOS> sheepNamesArray = new List<SheepClassIOS>();
        

        Random random = new Random();

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

        public ViewController(IntPtr handle) : base(handle)
        {
           
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            ButtonAddSheep.TouchUpInside += ButtonAddSheep_TouchUpInside;
            
        }

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
                
                if (sheepNamesArray.Contains(temp))
                {

                }

                var sheep = new SheepClassIOS();

                sheep.Name = temp;
                sheep.URL = Rand();

                sheepNamesArray.Add(sheep);
                listOfSheeps.Source = new TableSource(sheepNamesArray, this);
                listOfSheeps.ReloadData();
                count++;
                LabelNumberSheep.Text = count.ToString();
                textNameOfSheep.Text = "";
            }
        }

        public string Rand()
        {
            return sheepsStringURL[random.Next(0, 10)];
        }
    }
}
