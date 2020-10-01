using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class AnimalModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Killer { get; set; }
        public string WhoKilledMe { get; set; }
        public bool IsDead { get; set; }
        public int Order { get; set; }

        public AnimalModel()
        {

        }
    }
}
