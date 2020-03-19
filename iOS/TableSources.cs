using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class TableSource : UITableViewSource
    {
        List<string> sheepNamesArray = new List<string>();
        UIViewController controller;
        string SNA;
        public TableSource(List<string> items, UIViewController uIView)
        {
            sheepNamesArray = items;
            controller = uIView;
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
           
            SNA = sheepNamesArray[indexPath.Row];
            tableView.DeselectRow(indexPath, true);

            NabvigateTo();
        }

        public void NabvigateTo()
        {
            CardIDViewController cardIDViewController = controller.Storyboard.InstantiateViewController("CardIDViewController") as CardIDViewController;
            controller.NavigationController.PushViewController(cardIDViewController, true);
            cardIDViewController.SheepText = SNA;
        }
    }
}
