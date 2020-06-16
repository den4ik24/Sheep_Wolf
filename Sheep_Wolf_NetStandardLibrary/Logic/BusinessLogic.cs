using System.Collections.Generic;
using System.Linq;

namespace Sheep_Wolf_NetStandardLibrary
{
    public interface IBusinessLogic
    {
        List<AnimalModel> AnimalModel();
        bool AddAnimal(bool iS, string aN);
        void AddDucks();
        void GetListAnimals();
        AnimalModel GetAnimal(int animalID, int typeOfAnimal);
    }

    public class BusinessLogic : IBusinessLogic
    {
        IDataBase dataBase = new DataBase();
        AnimalModel animal;
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        int duckCount=1;
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

        public void AddDucks()
        {
            animal = DuckModel.GetDuck();
            animal.Order = animalModelsArray.Count;
            animal.Name = $"Duck_{duckCount++}";
            animalModelsArray.Add(animal);
            dataBase.Insert(animal);
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
                        break;
                    }

                    if(item is DuckModel)
                    {
                        animalModelsArray.Remove(item);
                        duckCount = 1;
                        break;
                    }
                }
            }
        }

        public void GetListAnimals()
        {
            animalModelsArray.AddRange(dataBase.SelectTable());
            animalModelsArray = animalModelsArray.OrderBy(a => a.Order).ToList();

        }

        public AnimalModel GetAnimal(int animalID, int typeOfAnimal)
        {

            var model = dataBase.Transfer(animalID, typeOfAnimal);
            return model;
        }
    }
}
