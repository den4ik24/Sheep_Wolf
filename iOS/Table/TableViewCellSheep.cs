using System;
using FFImageLoading;
using UIKit;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.iOS
{
    public partial class TableViewCellSheep : UITableViewCell
	{
		public TableViewCellSheep (IntPtr handle) : base (handle)
		{
		}

        public void UpdateCell(AnimalModel sheep)
        {
            textTableViewSheepsName.Text = sheep.Name;

            if (!sheep.IsDead)
            {
                ImageService.Instance.LoadUrl(sheep.URL).Into(fotoSheep);
                fotoSheep.ContentMode = UIViewContentMode.ScaleAspectFill;
            }
            else
            {
                fotoSheep.Image = UIImage.FromBundle("rip.png");
                fotoSheep.ContentMode = UIViewContentMode.ScaleAspectFit;
            }
        }
    }
}
