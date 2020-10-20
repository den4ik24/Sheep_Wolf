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
                UIImageView _imageStar = new UIImageView();
                _imageStar.HeightAnchor.ConstraintEqualTo(70).Active = true;
                _imageStar.WidthAnchor.ConstraintEqualTo(70).Active = true;
                starsLayout.AddArrangedSubview(_imageStar);
                _imageStar.Image = UIImage.FromBundle(Foto.STAR);
                _imageStar.ContentMode = UIViewContentMode.ScaleAspectFit;
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