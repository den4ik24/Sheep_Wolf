
namespace Sheep_Wolf_NetStandardLibrary
{
    public class AnimalModel
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Killer { get; set; }
        public bool IsDead { get; set; }
        public int Order { get; set; }

        public virtual string AnimalURL()
        {
            return URL;
        }
    }
}
