// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Sheep_Wolf.iOS
{
    [Register ("TableViewCellHunters")]
    partial class TableViewCellHunters
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView fotoHunter { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel textTableViewHuntersName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (fotoHunter != null) {
                fotoHunter.Dispose ();
                fotoHunter = null;
            }

            if (textTableViewHuntersName != null) {
                textTableViewHuntersName.Dispose ();
                textTableViewHuntersName = null;
            }
        }
    }
}