// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Sheep_Wolf.iOS
{
    [Register ("TableViewCellSheep")]
    partial class TableViewCellSheep
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView fotoSheep { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel textTableViewSheepsName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (fotoSheep != null) {
                fotoSheep.Dispose ();
                fotoSheep = null;
            }

            if (textTableViewSheepsName != null) {
                textTableViewSheepsName.Dispose ();
                textTableViewSheepsName = null;
            }
        }
    }
}