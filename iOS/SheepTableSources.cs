using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class TableSource : UITableViewSource
    {
        //UITapGestureRecognizer tapGesture;
        List<string> sheepNamesArray = new List<string>();

        public TableSource(List<string> items)
        {
            sheepNamesArray = items;
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (TableViewCell)tableView.DequeueReusableCell("cell");
            cell.UpdateCell(sheepNamesArray[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return sheepNamesArray.Count;
          
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {

            //tapGesture = new UITapGestureRecognizer(Tap);
            //tapGesture.NumberOfTapsRequired = 1;

            //base.RowSelected(tableView, indexPath);
            var SN = sheepNamesArray[indexPath.Row];
            tableView.DeselectRow(indexPath, true);

            //void Tap(UITapGestureRecognizer tap1)
            //{
            //    CardIDViewController cardIDViewController = Storyboard.InstantiateViewController("cardIDViewController") as CardIDViewController;
            //    NavigationController.PushViewController(cardIDViewController, true);
            //}

        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            cell.SeparatorInset = UIEdgeInsets.Zero;
            cell.LayoutMargins = UIEdgeInsets.Zero;
        }
    }
}
