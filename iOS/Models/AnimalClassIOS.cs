namespace Sheep_Wolf.iOS
{
    public class AnimalClassIOS
    {
        public string Name { get; set; }
        public string URL { get; protected set; }
        public string Killer { get; set; }
        public bool IsDead { get; set; }
        public bool Eater { get; set; }

        public virtual string GetRandomImage()
        {
            return "RandomImage";
        }
    }
}
