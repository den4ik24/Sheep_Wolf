using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheep_Wolf_NetStandardLibrary
{
    public interface IBusinessLogic
    {
        List<AnimalModel> AnimalModel();
        bool AddAnimal(int iS, string aN);
        void GetListAnimals();
        AnimalType TypeOfAnimal(AnimalModel animal);
        AnimalModel GetAnimal(int animalID, int typeOfAnimal);
        event EventHandler<string> Notify;
    }

    public class BusinessLogic : EventArgs, IBusinessLogic
    {
        IDataBase dataBase = new DataBase();
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        int duckCount;
        public event EventHandler<string> Notify;

        public List<AnimalModel> AnimalModel()
        {
            return animalModelsArray;
        }

        public bool AddAnimal(int isSheep, string animalName)
        {
            var animal = ChoiceAnimal(isSheep);
            if (isSheep is (int)AnimalType.DUCK ||
                isSheep is (int)AnimalType.SHEEP ||
                isSheep is (int)AnimalType.WOLF)
            {
                var repeatAnimal = animalModelsArray.Where(a => a.Name == animalName);
                if (repeatAnimal.Any())
                {
                    return true;
                }
                else
                {
                    if (animal is DuckModel)
                    {
                        animal.Name = $"Duck_{++duckCount}";
                        animalName = animal.Name;
                    }
                    ActionWithCreatures(animalName, animal);
                    return false;
                }
            }

            else
            {
                animalName = animal.Name;
                ActionWithCreatures(animalName, animal);
                return false;
            }
        }

        public AnimalModel ChoiceAnimal(int isSheep)
        {
            var type = (AnimalType)isSheep;
            AnimalModel animal;
            switch (type)
            {
                case AnimalType.SHEEP:
                    animal = SheepModel.GetSheep();
                    return animal;
                case AnimalType.WOLF:
                    animal = WolfModel.GetWolf();
                    return animal;
                case AnimalType.DUCK:
                    animal = DuckModel.GetDuck();
                    return animal;
                case AnimalType.HUNTER:
                    animal = HunterModel.GetHunter();
                    return animal;
                default:break;
            }
            return null;
        }

        public void AssignAnimal(string animalName, AnimalModel animal)
        {
            animal.Order = animalModelsArray.Count;
            animal.Name = animalName;
            animalModelsArray.Add(animal);
        }

        public void AnimalKiller(AnimalModel animal)
        {
            if (animal is WolfModel)
            {
                DuckFlyAway();

                for (var i = animalModelsArray.Count - 1; i >= 0; --i)
                {
                    var item = animalModelsArray[i];
                    //волки жрут овцу
                    if (item is SheepModel && !item.IsDead)
                    {
                        WhoKilledWho(item, animal);
                        Notify?.Invoke(this, $"Волк {animal.Name} сожрал овцу {item.Name}");
                        break;
                    }
                    //волки жрут охотника
                    if (item is HunterModel && !item.IsDead)
                    {
                        WhoKilledWho(item, animal);
                        Notify?.Invoke(this, $"Волк {animal.Name} разодрал охотника {item.Name}");
                        break;
                    }
                }
            }

            if (animal is HunterModel)
            {
                DuckFlyAway();

                for (var i = animalModelsArray.Count - 1; i >= 0; --i)
                {
                    var item = animalModelsArray[i];
                    //охотник валит волка
                    if (item is WolfModel && !item.IsDead)
                    {
                        WhoKilledWho(item, animal);
                        Notify?.Invoke(this, $"Охотник {animal.Name} завалил волка {item.Name}");
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

        public AnimalType TypeOfAnimal(AnimalModel animal)
        {
            AnimalType type;
            if (animal is SheepModel)
            {
                type = AnimalType.SHEEP;
            }
            else if(animal is WolfModel)
            {
                type = AnimalType.WOLF;
            }
            else if (animal is DuckModel)
            {
                type = AnimalType.DUCK;
            }
            else
            {
                type = AnimalType.HUNTER;
            }
            return type;
        }

        public AnimalModel GetAnimal(int animalID, int typeOfAnimal)
        {
            var type = (AnimalType)typeOfAnimal;
            switch (type)
            {
                case AnimalType.SHEEP:
                    return dataBase.GetItem<SheepModel>(animalID);
                case AnimalType.DUCK:
                    return dataBase.GetItem<DuckModel>(animalID);
                case AnimalType.WOLF:
                    return dataBase.GetItem<WolfModel>(animalID);
                case AnimalType.HUNTER:
                    return dataBase.GetItem<HunterModel>(animalID);
                default:
                    break;
            }
            return null;
        }

        public void DuckFlyAway()
        {
            animalModelsArray.RemoveAll(a => a is DuckModel);
            dataBase.Delete<DuckModel>();
            duckCount = 0;
        }

        public void WhoKilledWho(AnimalModel item, AnimalModel animal)
        {
            item.IsDead = true;
            item.WhoKilledMe = animal.Name;
            animal.Killer = item.Name;
            dataBase.Update(item);
            dataBase.Update(animal);
        }

        public void ActionWithCreatures(string animalName, AnimalModel animal)
        {
            AssignAnimal(animalName, animal);
            AnimalKiller(animal);
            dataBase.Insert(animal);
        }
    }
}
