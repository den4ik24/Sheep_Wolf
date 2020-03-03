using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class TableSource : UITableViewSource
    {
        List<string> sheepNamesArray;

        public TableSource(List<string> items)
        {
            sheepNamesArray = items;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("cell");

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, "cell");
            }
            cell.TextLabel.Text = sheepNamesArray[indexPath.Row];
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
