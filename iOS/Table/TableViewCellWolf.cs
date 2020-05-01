using System;
using FFImageLoading;
using Sheep_Wolf_NetStandardLibrary;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class TableViewCellWolf : UITableViewCell
	{
		public TableViewCellWolf (IntPtr handle) : base (handle)
		{
		}

        public void UpdateCell(AnimalModel animal)
        {
            textTableViewWolvesName.Text = animal.Name;
            ImageService.Instance.LoadUrl(animal.URL).Into(fotoWolf);
            Console.WriteLine(animal.URL);
        }
    }
}
