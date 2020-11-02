using System;
using FFImageLoading;
using UIKit;
using Sheep_Wolf_NetStandardLibrary;
using System.Linq;

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

            KillersName(animal);
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
                UIImageView _imageStar = new UIImageView();
                starsLayout.AddArrangedSubview(_imageStar);
                _imageStar.Image = UIImage.FromBundle(Foto.STAR);
                _imageStar.ContentMode = UIViewContentMode.ScaleAspectFit;
            }
        }

        public void KillersName(AnimalModel animal)
        {
            var name = businessLogic.NameofKiller(animal);
            if(name != "")
            {
                whoKillMe.Text = name;
                whoKillMe.Alpha = 1;

            }
            else
            {
                whoKillMe.Text = "";
                whoKillMe.Alpha = 0;
                NSLayoutConstraint.DeactivateConstraints(whoKillMe.Constraints);
                viewID.Constraints.FirstOrDefault(a => a.FirstItem == NameAnimalID && a.SecondItem == whoKillMe).Active = false;
                NameAnimalID.TopAnchor.ConstraintEqualTo(labelAnimalName.BottomAnchor, 10).Active = true;
            }
        }

        public void AddBottomImage(AnimalModel animal)
        {
            var load = ImageService.Instance.LoadUrl(animal.URL);
            if (animal.IsDead)
            {
                load.Transform(new GrayscaleTransformation());
            }
            load.Into(animalFoto);
        }
    }
}