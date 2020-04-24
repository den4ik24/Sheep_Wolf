using System;
using FFImageLoading;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
	public partial class TableViewCellSheep : UITableViewCell
	{
		public TableViewCellSheep (IntPtr handle) : base (handle)
		{
		}

        public void UpdateCell(AnimalClassIOS animal)
        {
            textTableViewSheepsName.Text = animal.Name;
            ImageService.Instance.LoadUrl(animal.URL).Into(fotoSheep);
        }
    }
}
