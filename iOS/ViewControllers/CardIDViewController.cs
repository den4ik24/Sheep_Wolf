using System;
using FFImageLoading;
using UIKit;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.iOS
{
    public partial class CardIDViewController : UIViewController
    {
        IBusinessLogic businessLogic = new BusinessLogic();
        public int animalId;
        public int type;
        public CardIDViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var animal = businessLogic.GetAnimal(animalId, type);

            labelAnimalName.Text = animal.Name;

            ImageService.Instance.LoadUrl(animal.URL).Into(animalFoto);

            if(animal is SheepModel)
            {
                ImageAnimal.Image = UIImage.FromBundle("sheep.png");
                NameAnimalID.Text = AnimalType.SHEEP.ToString();
            }
            else if(animal is WolfModel)
            {
                ImageAnimal.Image = UIImage.FromBundle("wolf.png");
                NameAnimalID.Text = AnimalType.WOLF.ToString();
            }

            else
            {
                ImageAnimal.Image = UIImage.FromBundle("duck.png");
                NameAnimalID.Text = AnimalType.DUCK.ToString();
            }

            if (animal.Killer != null)
            {
                if(animal is SheepModel)
                {
                    NameAnimalID.Text = $"This {AnimalType.SHEEP} eliminated by {animal.Killer}";
                }
                else if(animal is WolfModel)
                {
                    NameAnimalID.Text = $"This {AnimalType.WOLF} tear to pieces {animal.Killer}";
                    ImageAnimal.Image = UIImage.FromBundle("killer.png");
                }
            }

            if (animal.IsDead)
            {
                ImageAnimal.Image = UIImage.FromBundle("rip.png");
                ImageService.Instance.LoadUrl(animal.URL).Transform(new GrayscaleTransformation()).Into(animalFoto);
            }
        }
    }
}