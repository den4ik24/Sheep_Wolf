using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 0;
        List<string> sheepNamesArray = new List<string>();
        UITapGestureRecognizer tapGesture;
        
        public ViewController(IntPtr handle) : base(handle)
        {
           
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            ButtonAddSheep.TouchUpInside += ButtonAddSheep_TouchUpInside;

            tapGesture = new UITapGestureRecognizer(Tap);
            tapGesture.NumberOfTapsRequired = 1;

            //listOfSheeps[NSIndexPath.FromRowSection].AddGestureRecognizer(tapGesture);


        }

        void Tap(UITapGestureRecognizer tap1)
        {
            CardIDViewController cardIDViewController = Storyboard.InstantiateViewController("cardIDViewController") as CardIDViewController;
            NavigationController.PushViewController(cardIDViewController, true);
        }

        private void ButtonAddSheep_TouchUpInside(object sender, EventArgs e)
        {

            if (textNameOfSheep.Text == "")
            {

                var alertController = UIAlertController.Create
                    ("WARNING", "Введите имя овцы", UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create
                    ("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alertController, true, null);

            }
            else
            {
                sheepNamesArray.Add(textNameOfSheep.Text.ToString());
                listOfSheeps.Source = new TableSource(sheepNamesArray);
                listOfSheeps.TableFooterView = new UIView(CoreGraphics.CGRect.Empty);
                count++;
                LabelNumberSheep.Text = count.ToString();
                textNameOfSheep.Text = "";
            }
        }

        //public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        //{
        //    base.PrepareForSegue(segue, sender);
        //    var CardIDViewController = segue.DestinationViewController as CardIDViewController;
        //    if (CardIDViewController != null)
        //    {
        //        CardIDViewController.
        //    }
        //}


    }

}
