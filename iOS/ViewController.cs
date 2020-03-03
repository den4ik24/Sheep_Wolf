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

                //new UIAlertView("Warning", "Введите имя овцы", null, "OK", null).Show();

            }
            else
            {
                sheepNamesArray.Add(textNameOfSheep.Text.ToString());
                CustomTableView.Source = new TableSource(sheepNamesArray);
                //listOfSheeps.Source = new TableSource(sheepNamesArray);
                CustomTableView.TableFooterView = new UIView(CoreGraphics.CGRect.Empty);
                count++;
                LabelNumberSheep.Text = count.ToString();
                textNameOfSheep.Text = "";
            }
        } 

    }

    //public class TableSource : UITableViewSource
    //{
    //    List<string> sheepNamesArray;

    //    public TableSource(List<string> items)
    //    {
    //        sheepNamesArray = items;
    //    }

    //    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    //    {
    //        UITableViewCell cell = tableView.DequeueReusableCell("cell");

    //        if (cell == null)
    //        {
    //            cell = new UITableViewCell(UITableViewCellStyle.Default, "cell");
    //        }
    //        cell.TextLabel.Text = sheepNamesArray[indexPath.Row];
    //        return cell;
    //    }

    //    public override nint RowsInSection(UITableView tableview, nint section)
    //    {
    //        return sheepNamesArray.Count;
    //    }

    //    public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
    //    {
    //        cell.SeparatorInset = UIEdgeInsets.Zero;
    //        cell.LayoutMargins = UIEdgeInsets.Zero;
    //    }
    //}
}
