using System;
using System.Collections.Generic;
using FFImageLoading;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class TableSource : UITableViewSource
    {
        List<SheepClassIOS> sheepNamesArray = new List<SheepClassIOS>();
        UIViewController controller;

        public TableSource(List<SheepClassIOS> items, UIViewController uIView)
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

            tableView.DeselectRow(indexPath, true);
            var SHP = sheepNamesArray[indexPath.Row];

            NabvigateTo(SHP);
        }

        public void NabvigateTo(SheepClassIOS SHP)
        {
            CardIDViewController cardIDViewController = controller.Storyboard.InstantiateViewController("CardIDViewController") as CardIDViewController;
            controller.NavigationController.PushViewController(cardIDViewController, true);
            cardIDViewController.model = SHP;
        }

    }
}
