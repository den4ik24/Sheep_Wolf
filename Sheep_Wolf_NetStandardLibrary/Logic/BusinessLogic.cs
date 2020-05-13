using System.Collections.Generic;
using System.Linq;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class BusinessLogic
    {

        //public void animalList(AnimalModel animal)
        //{
        //    animalModelsArray.Add(animal);
        //    if (animal is WolfModel)
        //    {
        //        for (var i = animalModelsArray.Count - 1; i >= 0; --i)
        //        {
        //            var item = animalModelsArray[i];
        //            if (item is SheepModel && !item.IsDead)
        //            {
        //                item.IsDead = true;
        //                item.Killer = animal.Name;
        //                animal.Killer = item.Name;
        //                break;
        //            }
        //        }
        //    }
        //}

        //public bool AnimalListContain(AnimalModel animal)
        //{
        //    var repeatAnimal = animalModelsArray.Where(a => a.Name == animal.Name);
        //    return repeatAnimal.Any();
        //}

        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
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
                return false;
            }
        }
    }
}
