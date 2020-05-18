using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class BusinessLogic
    {
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        readonly string dbPath = Path.Combine(System.Environment.GetFolderPath
            (System.Environment.SpecialFolder.Personal), "dataBase.db3");

        public bool AddAnimal(bool isSheep, string animalName)
        {
            AnimalModel animal;

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
                    var connection = new SQLiteConnection(dbPath);
                    if (animal is SheepModel)
                    {
                        connection.CreateTable<SheepModel>();
                        connection.Insert(animal);
                        var table = connection.Table<SheepModel>();
                        foreach (var animalmodel in table)
                        {
                            System.Console.WriteLine($"\nзапись в базу Баранов: Имя - {animalmodel.Name}, Ссылка на фотку - {animalmodel.URL}");
                        }
                    }
                    if (animal is WolfModel)
                    {
                        connection.CreateTable<WolfModel>();
                        connection.Insert(animal);
                        var table = connection.Table<WolfModel>();
                        foreach (var animalmodel in table)
                        {
                            System.Console.WriteLine($"\nзапись в базу Волков: Имя - {animalmodel.Name}, Ссылка - {animalmodel.URL}");
                        }
                    }
                }
                catch(SQLiteException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                return false;
            }
        }
    }
}
