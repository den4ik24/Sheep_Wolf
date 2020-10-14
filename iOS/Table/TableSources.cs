using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.iOS
{
    public class TableSource : UITableViewSource
    {
        List<AnimalModel> animalClassArray;
        UIViewController controller;
        readonly IBusinessLogic businessLogic = new BusinessLogic();
        public TableSource(List<AnimalModel> animalModelsArray, UIViewController uIView)
        {
            animalClassArray = animalModelsArray;
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
            else if(animalClassArray[indexPath.Row] is DuckModel)
            {
                var cellDuck = tableView.DequeueReusableCell("cellOfDuck") as TableViewCellDuck;
                cellDuck.UpdateCell(animalClassArray[indexPath.Row]);
                return cellDuck;
            }
            else if(animalClassArray[indexPath.Row] is WolfModel)
            {
                var cellWolf = tableView.DequeueReusableCell("cellOfWolf") as TableViewCellWolf;
                cellWolf.UpdateCell(animalClassArray[indexPath.Row]);
                return cellWolf;
            }
            else
            {
                var cellHunter = tableView.DequeueReusableCell("cellOfHunter") as TableViewCellHunters;
                cellHunter.UpdateCell(animalClassArray[indexPath.Row]);
                return cellHunter;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return animalClassArray.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            var animal = animalClassArray[indexPath.Row];
            var animalId = animal.Id;
            var type = businessLogic.TypeOfAnimal(animal);
            NabvigateTo(animalId, (int)type);
        }

        public void NabvigateTo(string animalId, int type)
        {
            CardIDViewController cardIDViewController = controller.Storyboard.InstantiateViewController("CardIDViewController") as CardIDViewController;
            controller.NavigationController.PushViewController(cardIDViewController, true);
            cardIDViewController.animalId = animalId;
            cardIDViewController.type = type;
        }
    }
}
