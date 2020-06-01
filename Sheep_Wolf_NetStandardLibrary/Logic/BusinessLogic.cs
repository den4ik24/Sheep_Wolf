using System.Collections.Generic;
using System.Linq;

namespace Sheep_Wolf_NetStandardLibrary
{
    public interface IBusinessLogic
    {
        List<AnimalModel> AnimalModel();
        bool AddAnimal(bool isSheep, string animalName);
        void GetAnimals();
    }

    public class BusinessLogic : IBusinessLogic
    {
        IDataBase dataBase = new DataBase();
        AnimalModel animal;
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();

        public List<AnimalModel> AnimalModel()
        {
            return animalModelsArray;
        }

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
                animal = SheepModel.GetSheep();

            }
            else
            {
                animal = WolfModel.GetWolf();
            }
        }

        public void AssignAnimal(string animalName)
        {
            animal.Order = animalModelsArray.Count;
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
                        System.Console.WriteLine($"item.IsDead = {item.IsDead}, item.Killer = {item.Killer}, animal.Killer = {animal.Killer}");
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
