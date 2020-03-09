using Foundation;
using System;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class CardIDViewController : UIViewController
    {
        public string SheepText;

        public CardIDViewController (IntPtr handle) : base (handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

           labelSheepName.Text = SheepText;
        }
    }
}