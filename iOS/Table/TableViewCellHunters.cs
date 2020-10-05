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

        public void UpdateCell(AnimalModel hunter)
        {
            textTableViewHuntersName.Text = hunter.Name;

            if (!hunter.IsDead)
            {
                ImageService.Instance.LoadUrl(hunter.URL).Into(fotoHunter);
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