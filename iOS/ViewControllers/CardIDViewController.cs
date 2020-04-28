using System;
using FFImageLoading;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class CardIDViewController : UIViewController
    {
        public AnimalClassIOS model;
        public CardIDViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            labelAnimalName.Text = model.Name;

            ImageService.Instance.LoadUrl(model.URL).Into(animalFoto);

            if(model is SheepClassIOS)
            {
                ImageAnimal.Image = UIImage.FromBundle("sheep.png");
                NameAnimalID.Text = Keys.SHEEP;
            }
            else
            {
                ImageAnimal.Image = UIImage.FromBundle("wolf.png");
                NameAnimalID.Text = Keys.WOLF;
            }
        }
    }
}