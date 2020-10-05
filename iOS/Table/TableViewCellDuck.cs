using System;
using FFImageLoading;
using Sheep_Wolf_NetStandardLibrary;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class TableViewCellDuck : UITableViewCell
    {
        public TableViewCellDuck (IntPtr handle) : base (handle)
        {
        }

        public void UpdateCell(AnimalModel duck)
        {
            textTableViewDucksName.Text = duck.Name;
            ImageService.Instance.LoadUrl(duck.URL).Into(fotoDuck);
        }
    }
}