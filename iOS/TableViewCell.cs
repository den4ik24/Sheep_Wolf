using System;
using FFImageLoading;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class TableViewCell : UITableViewCell
    {
        public TableViewCell(IntPtr handle) : base(handle)
        {

        }

        //public void UpdateCell(SheepClassIOS sheep)
        //{
        //    textTableViewSheepsName.Text = sheep.Name;
        //    textTableViewSheep.Text = "SHEEP";
        //    ImageService.Instance.LoadUrl(sheep.URL).Into(fotoSheep);
        //}

        public void UpdateCell(AnimalClassIOS animal)
        {

                textTableViewSheepsName.Text = animal.Name;
                textTableViewSheep.Text = animal.Type;
                ImageService.Instance.LoadUrl(animal.URL).Into(fotoSheep);
            
        }
    }
}