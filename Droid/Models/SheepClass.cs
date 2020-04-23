using System;

namespace Sheep_Wolf.Droid
{
    public class SheepClass : AnimalClass
    {
        readonly Random random = new Random();

        public string[] sheepsStringURL =
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

        public string GetRandomSheepImage()
        {
            return sheepsStringURL[random.Next(0, 10)];
        }

        public override string GetRandomImage()
        {
            return GetRandomSheepImage();
        }

        public SheepClass()
        {
            URL = GetRandomImage();
        }
    }
}
