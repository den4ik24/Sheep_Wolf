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
            labelSheepName.Text = model.Name;

            ImageService.Instance.LoadUrl(model.URL).Into(sheepFoto);

            if(model is SheepClassIOS)
            {
                ImageSheep.Image = UIImage.FromBundle("sheep.png");
                NameAnimalID.Text = "SHEEP";
            }
            else
            {
                ImageSheep.Image = UIImage.FromBundle("wolf.png");
                NameAnimalID.Text = "WOLF";
            }
        }
    }
}