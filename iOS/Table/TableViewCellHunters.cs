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

            if (!animal.IsDead)
            {
                ImageService.Instance.LoadUrl(animal.URL).Into(fotoHunter);
                fotoHunter.ContentMode = UIViewContentMode.ScaleAspectFill;
            }
            else
            {
                fotoHunter.Image = UIImage.FromBundle("hunter-rip.png");
                fotoHunter.ContentMode = UIViewContentMode.ScaleAspectFit;
            }
        }
    }
}