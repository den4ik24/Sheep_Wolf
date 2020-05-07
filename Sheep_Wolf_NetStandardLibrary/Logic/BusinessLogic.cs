using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Widget;
using UIKit;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class BusinessLogic
    {
       public List<AnimalModel> animalModelsArray = new List<AnimalModel>();

        public void animalList(AnimalModel animal)
        {
            animalModelsArray.Add(animal);
            SheepAssignment.SheepIsDead(animal, animalModelsArray);
        }

        public bool AnimalListContain(AnimalModel animal)
        {
            var repeatAnimal = animalModelsArray.Where(a => a.Name.Contains(animal.Name));
            foreach(AnimalModel animals in repeatAnimal)
            {
                return true;
            }
            return false;
        }

        public void DataTransmission(AnimalModel N, Intent intent)
        {
            AnimalType type;
            if (N is SheepModel)
            {
                type = AnimalType.SHEEP;
            }
            else
            {
                type = AnimalType.WOLF;
            }

            intent.PutExtra(Keys.NAMEofANIMAL, N.Name);
            intent.PutExtra(Keys.FOTOofANIMAL, N.URL);
            intent.PutExtra(Keys.TYPEofANIMAL, (int)type);
            if (N.IsDead)
            {
                intent.PutExtra(Keys.DEADofANIMAL, N.IsDead);
            }
            if (!string.IsNullOrEmpty(N.Killer))
            {
                intent.PutExtra(Keys.KILLERofANIMAL, N.Killer);
            }
        }
    }
}
