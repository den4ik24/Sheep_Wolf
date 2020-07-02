using System;
using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    [Table("Hunters")]
    public class HunterModel : AnimalModel
    {
        readonly static Random random = new Random();
        public static string[] hunterStringURL =
        {
            "https://ohotagazeta.ru/wp-content/uploads/2019/09/grouse-xlarge_trans_NvBQzQNjv4BqKutH9T4LB0mU7ExMWwW8NNRIAe4E5iIN5B7rPrfGGGk.jpg",
            "https://khersonmasternews.com.ua/uploads/posts/ohotnikam-iz-ni-puha-ni-pera-vydelili-zemlyu-na-hersonschine.jpg",
            "https://guncarrier.com/wp-content/uploads/2018/06/hunter-aiming-rifle-forest-.22-long-rifle-ss-Feature.jpg",
            "https://avatars.mds.yandex.net/get-pdb/2074020/953dd8e4-c13f-491b-aa2c-865c774ddf8d/s1200",
            "https://wildlife.by/upload/medialibrary/273/273764662dc88818a89084b26c32e101.jpeg",
            "https://chastnik.ru/upload/iblock/97c/97c1999dd502856b2488a015a61e5322.jpg",
            "https://moi-goda.ru/images/opengraph/gradient_article_172814.jpg",
            "https://big-rostov.ru/wp-content/uploads/2018/10/46643364.jpg",
            "https://pbs.twimg.com/media/CgUY4yZXEAA6fPk.jpg",
            "https://dela.ru/medianew/img/11-9222929.jpg"
        };

        public static string[] hunterStringName =
        {
            "Пал Палыч",
            "Тимофей Емельянович",
            "Федор Димитриевич",
            "Олег Николаевич",
            "Антон Павлович",
            "Александр Сергеевич",
            "Лев Николаевич",
            "Фёдор Иванович",
            "Афанасий Афанасьевич",
            "Алексей Масксимович"
        };
        public static AnimalModel GetHunter()
        {
            var hunter = new HunterModel();
            hunter.URL = GetRandomImage();
            hunter.Name = GetRandomName();
            return hunter;
        }

        public static string GetRandomImage()
        {
            return hunterStringURL[random.Next(0, 10)];
        }

        public static string GetRandomName()
        {
            return hunterStringName[random.Next(0, 10)];
        }
    }
}
