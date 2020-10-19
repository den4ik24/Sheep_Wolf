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
            //ImageService.Instance.LoadUrl(animal.URL).Into(animalFoto);

            whoKillMe.Text = businessLogic.NameofKiller(animal);
            NameAnimalID.Text = businessLogic.TextKill(animal);
            var animalState = businessLogic.GetAnimalState(animal);
            ImageAnimal.Image = UIImage.FromBundle(AnimalModelImager.GetAnimalImage(animal, animalState));
            StarPicture(star);
            AddBottomImage(animal);
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

        public void AddBottomImage(AnimalModel animal)
        {
            var load = ImageService.Instance.LoadUrl(animal.URL);
            if (animal.IsDead)
            {
                load.Transform(new GrayscaleTransformation());
                //ImageService.Instance.LoadUrl(animal.URL).Transform(new GrayscaleTransformation()).Into(animalFoto);
            }
            load.Into(animalFoto);
        }
    }

    public static class AnimalModelImager
    {
        public static string GetAnimalImage(AnimalModel model, AnimalState state)
        {
            if (model is SheepModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return "sheep.png";
                }
                if (state == AnimalState.DEAD)
                {
                    return "rip.png";
                }
            }

            if (model is WolfModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return "wolf.png";
                }
                if (state == AnimalState.DEAD)
                {
                    return "wolf-rip.png";
                }
                if (state == AnimalState.KILLER)
                {
                    return "killer.png";
                }
            }

            if (model is HunterModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return "hunter.png";
                }
                if (state == AnimalState.DEAD)
                {
                    return "hunter-rip.png";
                }
                if (state == AnimalState.KILLER)
                {
                    return "hunter_killer.png";
                }
            }

            if (model is DuckModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return "duck.png";
                }
            }

            throw new Exception();
        }
    }
}