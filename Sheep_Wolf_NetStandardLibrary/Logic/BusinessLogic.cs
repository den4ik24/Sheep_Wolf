using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using System;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class BusinessLogic
    {
        DataBase dataBase = new DataBase();
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        //private readonly string dbPath = Path.Combine(Environment.GetFolderPath
        //    (Environment.SpecialFolder.Personal), "dataBase.db3");

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
                dataBase.Connection(animal);
                //try
                //{
                    //var connection = new SQLiteConnection(dbPath);
                    //if (animal is SheepModel)
                    //{
                        //connection.Insert(animal);
                    //}
                    //if (animal is WolfModel)
                    //{
                    //    connection.Insert(animal);
                    //}
                //}
                //catch(SQLiteException ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                return false;
            }
        }

        public void SelectTableBL()
        {
            dataBase.SelectTable(animalModelsArray);
        }
    }
}
