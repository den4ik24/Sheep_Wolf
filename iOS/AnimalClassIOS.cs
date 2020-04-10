using System;
namespace Sheep_Wolf.iOS
{
    public class AnimalClassIOS
    {
        public virtual string Name { get; set; }
        public virtual string URL { get; protected set; }

        public virtual string GetRandomImage()
        {
            return "RandomImage";
        }
    }
}
