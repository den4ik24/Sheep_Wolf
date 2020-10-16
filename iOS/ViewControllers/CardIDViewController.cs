using System;
using FFImageLoading;
using UIKit;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.iOS
{
    public partial class CardIDViewController : UIViewController
    {
        IBusinessLogic businessLogic = new BusinessLogic();
        IDataBase dataBase = new DataBase();
        public string animalId;
        public int type;
        public CardIDViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var animal = businessLogic.GetAnimal(animalId, type);
            var star = dataBase.GetID<Prey>(animalId);
            labelAnimalName.Text = animal.Name;

            ImageService.Instance.LoadUrl(animal.URL).Into(animalFoto);

            if(animal is SheepModel)
            {
                ImageAnimal.Image = UIImage.FromBundle("sheep.png");
                NameAnimalID.Text = AnimalType.SHEEP.ToString();
            }
            if(animal is WolfModel)
            {
                ImageAnimal.Image = UIImage.FromBundle("wolf.png");
                NameAnimalID.Text = AnimalType.WOLF.ToString();
            }

            if(animal is HunterModel)
            {
                ImageAnimal.Image = UIImage.FromBundle("hunter.png");
                NameAnimalID.Text = AnimalType.HUNTER.ToString();
            }

            if(animal is DuckModel)
            {
                ImageAnimal.Image = UIImage.FromBundle("duck.png");
                NameAnimalID.Text = AnimalType.DUCK.ToString();
            }

            if (animal.Killer != null)
            {
                if(animal is WolfModel)
                {
                    NameAnimalID.Text = $"This {AnimalType.WOLF} tear to pieces {animal.Killer}";
                    ImageAnimal.Image = UIImage.FromBundle("killer.png");
                    StarPicture(star);
                }
                if(animal is HunterModel)
                {
                    NameAnimalID.Text = $"This {AnimalType.HUNTER} just kill a {animal.Killer}";
                    ImageAnimal.Image = UIImage.FromBundle("hunter_killer.png");
                    StarPicture(star);
                }
            }

            if (animal.WhoKilledMe != null)
            {
                if (animal is SheepModel)
                {
                    whoKillMe.Text = $"This {AnimalType.SHEEP} eliminated by {animal.WhoKilledMe}";
                }
                if (animal is WolfModel)
                {
                    whoKillMe.Text = $"This {AnimalType.WOLF} is killed by a hunter {animal.WhoKilledMe}";
                }
                if (animal is HunterModel)
                {
                    whoKillMe.Text = $"This {AnimalType.HUNTER} is tear to pieces by a wolf {animal.WhoKilledMe}";
                }
            }

            if (animal.IsDead)
            {
                if(animal is SheepModel)
                {
                    ImageAnimal.Image = UIImage.FromBundle("rip.png");
                }
                if(animal is WolfModel)
                {
                    ImageAnimal.Image = UIImage.FromBundle("wolf-rip.png");
                }
                if(animal is HunterModel)
                {
                    ImageAnimal.Image = UIImage.FromBundle("hunter-rip.png");
                }
                ImageService.Instance.LoadUrl(animal.URL).Transform(new GrayscaleTransformation()).Into(animalFoto);
            }
        }

        public void StarPicture(int star)
        {
            for (int i = 0; i < star; i++)
            {
                UIImageView imageStar = new UIImageView();
                imageStar.HeightAnchor.ConstraintEqualTo(70).Active = true;
                imageStar.WidthAnchor.ConstraintEqualTo(70).Active = true;
                starsLayout.AddArrangedSubview(imageStar);
                imageStar.Image = UIImage.FromBundle("star.png");
                imageStar.ContentMode = UIViewContentMode.ScaleAspectFit;
            }
        }
    }
}