using System;
using FFImageLoading;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
	public partial class TableViewCellWolf : UITableViewCell
	{
		public TableViewCellWolf (IntPtr handle) : base (handle)
		{
		}

        public void UpdateCell(AnimalClassIOS animal)
        {
            textTableViewWolvesName.Text = animal.Name;
            ImageService.Instance.LoadUrl(animal.URL).Into(fotoWolf);
        }
    }
}
