using System;
using System.Collections.Generic;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 0;
        List<string> sheepNamesArray = new List<string>();

        public ViewController(IntPtr handle) : base(handle)
        {
           
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            ButtonAddSheep.TouchUpInside += ButtonAddSheep_TouchUpInside;

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
                listOfSheeps.Source = new TableSource(sheepNamesArray, this);
                listOfSheeps.ReloadData();
                count++;
                LabelNumberSheep.Text = count.ToString();
                textNameOfSheep.Text = "";
            }
        }

    }
}
