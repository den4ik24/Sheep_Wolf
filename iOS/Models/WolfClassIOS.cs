using System;
namespace Sheep_Wolf.iOS
{
    public class WolfClassIOS : AnimalClassIOS
    {
        readonly Random random = new Random();
        public string[] wolfStringURL =
        {
            "https://img5.goodfon.ru/original/1920x1408/1/35/volk-khishchnik-vzgliad-1.jpg",//
            "https://imgfon.ru/Images/Download/Crop/2560x1600/Animals/volk-hischnik-vzglyad-sherst-lejit.jpg",
            "https://www.chainimage.com/images/animal-hd-wallpaper-1600x1200-18-hebus-org-high-definition.jpg",
            "https://img1.liveinternet.ru/images/attach/c/6/90/875/90875795_1262537644_wolf_03_.jpg",
            "https://i.artfile.me/wallpaper/07-09-2017/1920x1280/zhivotnye-volki--kojoty--shakaly-vzglyad-1224870.jpg",
            "https://s00.yaplakal.com/pics/pics_original/4/0/0/13729004.jpg",
            "https://s1.1zoom.ru/b5050/650/301403-Sepik_2880x1800.jpg",//
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

        public WolfClassIOS()
        {
            URL = GetRandomImage();
        }
    }
}
