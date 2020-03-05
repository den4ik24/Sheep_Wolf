using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class TableSource : UITableViewSource
    {
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

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            cell.SeparatorInset = UIEdgeInsets.Zero;
            cell.LayoutMargins = UIEdgeInsets.Zero;
        }
    }
}
