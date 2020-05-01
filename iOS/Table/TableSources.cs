using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.iOS
{
    public class TableSource : UITableViewSource
    {
        List<AnimalModel> animalClassArray = new List<AnimalModel>();
        UIViewController controller;  

        public TableSource(List<AnimalModel> itemsAnimal, UIViewController uIView)
        {
            animalClassArray = itemsAnimal;
            controller = uIView;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (animalClassArray[indexPath.Row] is SheepModel)
            {
                var cellSheep = tableView.DequeueReusableCell("cellOfSheep") as TableViewCellSheep;
                cellSheep.UpdateCell(animalClassArray[indexPath.Row]);
                return cellSheep;
            }
            else
            {
                var cellWolf = tableView.DequeueReusableCell("cellOfWolf") as TableViewCellWolf;
                cellWolf.UpdateCell(animalClassArray[indexPath.Row]);
                return cellWolf;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return animalClassArray.Count; //listOfSheeps.ReloadData()
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            var NML = animalClassArray[indexPath.Row];
            NabvigateTo(NML);
        }

        public void NabvigateTo(AnimalModel AIA)
        {
            CardIDViewController cardIDViewController = controller.Storyboard.InstantiateViewController("CardIDViewController") as CardIDViewController;
            controller.NavigationController.PushViewController(cardIDViewController, true);
            cardIDViewController.model = AIA;
        }
    }
}
