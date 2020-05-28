using System.Collections.Generic;
using System.Linq;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class BusinessLogic
    {
        readonly DataBase dataBase = new DataBase();
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        AnimalModel animal;

        public bool AddAnimal(bool isSheep, string animalName)
        {
            var repeatAnimal = animalModelsArray.Where(a => a.Name == animalName);
            if (repeatAnimal.Any())
            {
                return true;
            }
            else
            {
                ChoiceAnimal(isSheep);
                AssignAnimal(animalName);
                AnimalKiller();
                dataBase.Insert(animal);
                return false;
            }
        }

        public void ChoiceAnimal(bool isSheep)
        {
            if (isSheep)
            {
                animal = new SheepModel();
            }
            else
            {
                animal = new WolfModel();
            }
        }

        public void AssignAnimal(string animalName)
        {
            animal.Order = animalModelsArray.Count;
            animal.AnimalURL();
            animal.Name = animalName;
            animalModelsArray.Add(animal);
        }

        public void AnimalKiller()
        {
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
        }

        public void GetAnimals()
        {
            animalModelsArray.AddRange(dataBase.SelectTable());
            animalModelsArray = animalModelsArray.OrderBy(a => a.Order).ToList();
        }
    }
}
