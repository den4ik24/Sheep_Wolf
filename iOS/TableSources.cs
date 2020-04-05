using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class TableSource : UITableViewSource
    {
        List<AnimalClassIOS> animalClassArray = new List<AnimalClassIOS>();
        UIViewController controller;

        public TableSource(List<AnimalClassIOS> itemsSheep, UIViewController uIView)
        {
            animalClassArray = itemsSheep;
            controller = uIView;
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (TableViewCell)tableView.DequeueReusableCell("cell");
            cell.UpdateCell(animalClassArray[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return animalClassArray.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            var NML = animalClassArray[indexPath.Row];
            NabvigateTo(NML);
        }

        public void NabvigateTo(AnimalClassIOS AIA)
        {
            CardIDViewController cardIDViewController = controller.Storyboard.InstantiateViewController("CardIDViewController") as CardIDViewController;
            controller.NavigationController.PushViewController(cardIDViewController, true);
            cardIDViewController.model = AIA;
        }

    }
}
