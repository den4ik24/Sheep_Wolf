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

        public void UpdateCell(AnimalModel wolf)
        {
            textTableViewWolvesName.Text = wolf.Name;

            if (!wolf.IsDead)
            {
                ImageService.Instance.LoadUrl(wolf.URL).Into(fotoWolf);
                fotoWolf.ContentMode = UIViewContentMode.ScaleAspectFill;
            }
            else
            {
                fotoWolf.Image = UIImage.FromBundle("wolf-rip.png");
                fotoWolf.ContentMode = UIViewContentMode.ScaleAspectFit;
            }
        }
    }
}
