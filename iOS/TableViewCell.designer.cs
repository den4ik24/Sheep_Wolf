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
    [Register ("TableViewCell")]
    partial class TableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView fotoSheep { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel textTableViewSheep { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel textTableViewSheepsName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (fotoSheep != null) {
                fotoSheep.Dispose ();
                fotoSheep = null;
            }

            if (textTableViewSheep != null) {
                textTableViewSheep.Dispose ();
                textTableViewSheep = null;
            }

            if (textTableViewSheepsName != null) {
                textTableViewSheepsName.Dispose ();
                textTableViewSheepsName = null;
            }
        }
    }
}