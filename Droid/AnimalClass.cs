﻿namespace Sheep_Wolf.Droid
{
    public class AnimalClass
    {
        public string Name { get; set; }
        public string URL { get; protected set; }
        public bool IsDead { get; set; }
        public string Killer { get; set; }
        public bool Eater { get; set; }

        public virtual string GetRandomImage()
        {
            return "RandomImage";
        }
    }
}
