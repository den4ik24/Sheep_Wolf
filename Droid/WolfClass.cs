using System;

namespace Sheep_Wolf.Droid
{
    public class WolfClass : AnimalClass
    {
        readonly Random random = new Random();

        public string[] wolfStringURL =
        {
            "https://www.proza.ru/pics/2014/03/17/1922.jpg",
            "https://imgfon.ru/Images/Download/Crop/2560x1600/Animals/volk-hischnik-vzglyad-sherst-lejit.jpg",
            "https://www.chainimage.com/images/animal-hd-wallpaper-1600x1200-18-hebus-org-high-definition.jpg",
            "https://wallpaperbro.com/img/256362.jpg",
            "https://i.artfile.me/wallpaper/07-09-2017/1920x1280/zhivotnye-volki--kojoty--shakaly-vzglyad-1224870.jpg",
            "https://s00.yaplakal.com/pics/pics_original/4/0/0/13729004.jpg",
            "https://i.ytimg.com/vi/GKK-nxCjSWc/maxresdefault.jpg",
            "https://image.wallperz.com/wp-content/uploads/2017/09/26/wallperz.com-20170926100049.jpg",
            "https://www.wallpaperup.com/uploads/wallpapers/2015/05/28/702184/fad311d0532eb1d00d28a093bd4abf8d-1400.jpg",
            "https://www.3d-hdwallpaper.com/wp-content/uploads/2019/05/desktop-free-wolf-wallpaper-download.jpg"
        };

        public string GetRandomWolfImage()
        {
            return wolfStringURL[random.Next(0, 10)];
        }

        public override string GetRandomImage()
        {
            return GetRandomWolfImage();
        }

        public WolfClass()
        {
            URL = GetRandomImage();
        }
    }
}
