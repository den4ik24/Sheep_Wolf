using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using System;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class BusinessLogic
    {
        readonly DataBase dataBase = new DataBase();
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        AnimalModel animal;
        int animalsCount;

        public bool AddAnimal(bool isSheep, string animalName)
        {
            if (isSheep)
            {
                animal = new SheepModel();
                animal.URL = animal.GetRandomImage();
            }
            else
            {
                animal = new WolfModel();
                animal.URL = animal.GetRandomImage();
            }

            var repeatAnimal = animalModelsArray.Where(a => a.Name == animalName);
            if (repeatAnimal.Any())
            {
                return true;
            }
            else
            {
                var animalOrder = animalModelsArray.Where(a => a.Order != 0);
                foreach (AnimalModel animal in animalOrder)
                {
                    animalsCount = animal.Order;
                }
                animalsCount++;
                animal.Order = animalsCount;
                
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
                dataBase.Insert(animal);
                return false;
            }
        }

        public void SelectTable_BL()
        {
            animalModelsArray.AddRange(dataBase.SelectTable());
            animalModelsArray.OrderBy(a => a.Order).ToList();
        }
    }
}
