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
                //NameAnimalID.Constraints.SetValue(whoKillMe, 10);
                //NameAnimalID.UpdateConstraintsIfNeeded();
                //NameAnimalID.UpdateConstraints();
                //NameAnimalID.SetNeedsUpdateConstraints();
            }
            else
            {
                whoKillMe.Text = "";
                whoKillMe.Alpha = 0;
                NSLayoutConstraint.DeactivateConstraints(whoKillMe.Constraints);

                whoKillMe.HeightAnchor.ConstraintEqualTo(0).Active = true;

                var c = viewID.Constraints.FirstOrDefault(a => a.FirstItem == whoKillMe && a.SecondItem == labelAnimalName);
                c.Active = false;

                //var b = viewID.Constraints.FirstOrDefault(a =>
                //{
                //    if (a.FirstItem == whoKillMe)
                //    {
                //        return true;
                //    }
                //    if (a.SecondItem == labelAnimalName)
                //    {
                //        return true;
                //    }
                //    return false;
                //});

                //b.Active = false;

                var b = viewID.Constraints.FirstOrDefault(a => a.FirstItem == NameAnimalID && a.SecondItem == whoKillMe);
                b.Active = false;

                NameAnimalID.TopAnchor.ConstraintEqualTo(labelAnimalName.BottomAnchor, 10).Active = true;

                //whoKillMe.BottomAnchor.ConstraintEqualTo(NameAnimalID.TopAnchor, 0).Active = true;


                //var obj = (new[]{ new{ Name = 1, Age = 2 }, new{ Name = 2, Age =3} }).ToList();
                //obj.Where(a => a.Age == 3);
                //foreach (var o in obj)
                //{
                //    if (o.Age == 3)
                //    {
                //        Console.WriteLine("!!!");
                //    }
                //}

                //for (int i = 0; i < obj.Length; i++)
                //{
                //    var o = obj[i];
                //    if (o.Age == 3)
                //    {
                //        break;
                //    }
                //}

                //whoKillMe.HeightAnchor.ConstraintEqualTo(70).Active = true;
                //whoKillMe.TrailingAnchor.ConstraintEqualTo(viewID.TrailingAnchor, 0).Active = true;
                //whoKillMe.LeadingAnchor.ConstraintEqualTo(ImageAnimal.TrailingAnchor, 0).Active = true;
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