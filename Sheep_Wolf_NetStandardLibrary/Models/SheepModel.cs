using System;
using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    [Table("Sheeps")]
    public class SheepModel : AnimalModel
    {
        readonly static Random random = new Random();
        public static string[] sheepsStringURL =
        {
            "https://steemitimages.com/DQmY5jSVRybkLgwyoD8apfXDUsm3Baj2ryahwUCwuVrcS7j/lamb-2146961_1920.jpg",
            "https://www.studentofthegun.com/wp-content/uploads/2017/10/SOTG_679_-_A_Nation_of_Sheep.jpg",
            "https://www.kidsbooksandpuppets.com/assets/images/folkmanislongwoolsheeppuppets2982slg.jpg",
            "https://www.parcs-zoologiques-lumigny.fr/wp-content/uploads/2019/03/mouton-1240.jpg",
            "http://risovach.ru/upload/2014/04/generator/naivnaya-ovechka2_48043820_orig_.jpeg",
            "https://yesofcorsa.com/wp-content/uploads/2017/04/4K-Sheep-Wallpaper-Gallery.jpg",
            "http://milifamily.pl/wp-content/uploads/2016/07/Untitled-design-18.jpg",
            "https://www.tokkoro.com/picsup/3313963-sheep-lamb-wool-mother.jpg",
            "https://www.focus.pl/uploads/media/default/0001/24/dolly.jpeg",
            "https://pix.avax.news/avaxnews/69/5c/00015c69.jpeg"
        };

        public static AnimalModel GetSheep()
        {
            var sheep = new SheepModel();
            sheep.URL = GetRandomImage();
            return sheep;
        }

        public static string GetRandomImage()
        {
            return sheepsStringURL[random.Next(0, 10)];
        }
    }
}
