using System;
using FFImageLoading;
using UIKit;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.iOS
{
    public partial class CardIDViewController : UIViewController
    {
        public AnimalModel model;
        public CardIDViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            labelAnimalName.Text = model.Name;

            ImageService.Instance.LoadUrl(model.URL).Into(animalFoto);

            if(model is SheepModel)
            {
                ImageAnimal.Image = UIImage.FromBundle("sheep.png");
                NameAnimalID.Text = AnimalType.SHEEP.ToString();
            }
            else if(model is WolfModel)
            {
                ImageAnimal.Image = UIImage.FromBundle("wolf.png");
                NameAnimalID.Text = AnimalType.WOLF.ToString();
            }

            else
            {
                ImageAnimal.Image = UIImage.FromBundle("duck.png");
                NameAnimalID.Text = AnimalType.DUCK.ToString();
            }

            if (model.Killer != null)
            {
                if(model is SheepModel)
                {
                    NameAnimalID.Text = $"This {AnimalType.SHEEP} eliminated by {model.Killer}";
                }
                if(model is WolfModel)
                {
                    NameAnimalID.Text = $"This {AnimalType.WOLF} tear to pieces {model.Killer}";
                    ImageAnimal.Image = UIImage.FromBundle("killer.png");
                }
            }

            if (model.IsDead)
            {
                ImageAnimal.Image = UIImage.FromBundle("rip.png");
                ImageService.Instance.LoadUrl(model.URL).Transform(new GrayscaleTransformation()).Into(animalFoto);
            }
        }
    }
}