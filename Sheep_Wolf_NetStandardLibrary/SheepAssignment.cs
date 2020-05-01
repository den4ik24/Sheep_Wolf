using System;
using System.Collections.Generic;

namespace Sheep_Wolf_NetStandardLibrary
{
    public static class SheepAssignment
    {
        public static void SheepIsDead(AnimalModel animal, List<AnimalModel> animalModelsArray)
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
    }
}
