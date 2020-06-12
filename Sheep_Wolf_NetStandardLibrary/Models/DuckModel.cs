using System;
using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    [Table("Ducks")]
    public class DuckModel : AnimalModel
    {
        readonly static Random random = new Random();
        public static string[] duckStringURL =
        {
            "https://images-eds-ssl.xboxlive.com/image?url=wHwbXKif8cus8csoZ03RW_ES.ojiJijNBGRVUbTnZKsoCCCkjlsEJrrMqDkYqs3MkfUCX8zOTc6CY2adKHOootgPynNSnTWZ7XQ4CBT_zF25VGnw2brLgYm3OMnnJNa1751q_DvBgDC7TuZE8JWnEIUJXnEeIVSdwuof6meNMxM-&format=png",
            "https://avatars.mds.yandex.net/get-zen_doc/1852570/pub_5d24993b23371c00adb65c80_5d24998b8da1ce00acfd8f63/scale_1200",
            "https://3.bp.blogspot.com/-sSu4Cs9wFN4/TsGSpCJrF9I/AAAAAAAAAOI/MQYNSWL9Lt8/s1600/Tapety+ms+%25284%2529.jpg",
            "https://avatars.mds.yandex.net/get-pdb/1817937/cf5cc35e-9b6e-4b45-99bd-bcfcd9645984/s1200?webp=false",
            "https://avatars.mds.yandex.net/get-pdb/1599133/794361b8-34ee-4290-b0f8-bbe3d0ab5c71/s1200?webp=false",
            "https://avatars.mds.yandex.net/get-pdb/921693/7d11afbc-3d0d-4699-b470-a1cf62e762aa/s1200?webp=false",
            "https://live.staticflickr.com/880/26783587597_f453a9e605_b.jpg",
            "https://cdn.photosight.ru/img/c/404/4929146_xlarge.jpg",
            "https://cdn.photosight.ru/img/b/e26/5871596_xlarge.jpg",
            "http://img2.3lian.com/2014cf/f2/30/d/82.jpg"
        };

        public static AnimalModel GetDuck()
        {
            var duck = new DuckModel();
            duck.URL = GetRandomImage();
            return duck;
        }

        public static string GetRandomImage()
        {
            return duckStringURL[random.Next(0, 10)];
        }
    }
}
