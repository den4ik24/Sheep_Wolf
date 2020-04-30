using System;
using FFImageLoading;
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

            if (!animal.IsDead)
            {
                fotoSheep.ContentMode = UIViewContentMode.ScaleAspectFill;
                textTableViewSheepsName.Text = animal.Name;
                ImageService.Instance.LoadUrl(animal.URL).Into(fotoSheep);
            }
            else
            {
                
                fotoSheep.Image = UIImage.FromBundle("rip.png");
                fotoSheep.ContentMode = UIViewContentMode.ScaleAspectFit;
            }
        }
    }
}
