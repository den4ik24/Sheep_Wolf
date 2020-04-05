using System;
namespace Sheep_Wolf.Droid
{
    public class AnimalClass
    {
        public virtual string Name { get; set; }
        public virtual string URL { get; protected set; }
        public virtual string Type
        {
            get
            {
                return "ANIMAL";
            }
        }

        public virtual string GetRandomImage()
        {
            return "RandomImage";
        }
    }
}
