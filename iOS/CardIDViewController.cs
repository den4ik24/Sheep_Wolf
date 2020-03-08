using Foundation;
using System;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class CardIDViewController : UIViewController
    {

        public CardIDViewController (IntPtr handle) : base (handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //labelSheepName.Text = textTableViewSheepsName.Text;
            ViewController viewController = Storyboard.InstantiateViewController("viewController") as ViewController;
            NavigationController.PushViewController(viewController, true);
        }
    }
}