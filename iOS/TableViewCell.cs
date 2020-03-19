using System;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class TableViewCell : UITableViewCell
    {
        public TableViewCell (IntPtr handle) : base (handle)
        {    
        }

        public void UpdateCell(string name)
        {
            textTableViewSheepsName.Text = name;
            textTableViewSheep.Text = "Sheep";
        }
        
    }
}