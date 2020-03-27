using System;
using FFImageLoading;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class CardIDViewController : UIViewController
    {
        public SheepClassIOS model;

        public CardIDViewController (IntPtr handle) : base (handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            labelSheepName.Text = model.Name;
            NameAnimalID.Text = model.Type;

            ImageService.Instance.LoadUrl(model.URL).Into(sheepFoto);
        }
    }
}