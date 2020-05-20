using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class BusinessLogic
    {
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        string dbPath = Path.Combine(System.Environment.GetFolderPath
            (System.Environment.SpecialFolder.Personal), "dataBase.db3");
            AnimalModel animal;
        SQLiteConnection connection;
        public bool AddAnimal(bool isSheep, string animalName)
        {
            if (isSheep)
            {
                animal = new SheepModel();
            }
            else
            {
                animal = new WolfModel();
            }

            var repeatAnimal = animalModelsArray.Where(a => a.Name == animalName);
            if (repeatAnimal.Any())
            {
                return true;
            }
            else
            {
                animal.Name = animalName;
                animalModelsArray.Add(animal);
                if (animal is WolfModel)
                {
                    for (var i = animalModelsArray.Count - 1; i >= 0; --i)
                    {
                        var item = animalModelsArray[i];
                        if (item is SheepModel && !item.IsDead)
                        {
                            item.IsDead = true;
                            item.Killer = animal.Name;
                            animal.Killer = item.Name;
                            break;
                        }
                    }
                }
                try
                {
                    connection = new SQLiteConnection(dbPath);
                    if (animal is SheepModel)
                    {
                        connection.CreateTable<SheepModel>();
                        connection.Insert(animal);
                    }
                    if (animal is WolfModel)
                    {
                        connection.CreateTable<WolfModel>();
                        connection.Insert(animal);
                    }
                }
                catch(SQLiteException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                return false;
            }
        }
        public bool SelectTable()
        {

            connection = new SQLiteConnection(dbPath);

            var tableSheep = connection.Table<SheepModel>().ToList();
            var tableWolf = connection.Table<WolfModel>().ToList();

            var animalArray = tableSheep.Union<AnimalModel>(tableWolf).ToList();
            animalModelsArray.AddRange(animalArray);

            //foreach (var animal in animalArray)
            //{
            //    animalModelsArray.Add(animal);
            //}
            return true;
        }
    }
}
