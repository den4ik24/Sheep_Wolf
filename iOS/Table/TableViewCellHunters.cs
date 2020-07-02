using System;
using FFImageLoading;
using Sheep_Wolf_NetStandardLibrary;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class TableViewCellHunters : UITableViewCell
    {
        public TableViewCellHunters (IntPtr handle) : base (handle)
        {
        }

        public void UpdateCell(AnimalModel animal)
        {
            textTableViewHuntersName.Text = animal.Name;

            ImageService.Instance.LoadUrl(animal.URL).Into(fotoHunter);
        }
    }
}