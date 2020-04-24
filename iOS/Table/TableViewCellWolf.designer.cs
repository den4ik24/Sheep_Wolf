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
    [Register ("TableViewCellWolf")]
    partial class TableViewCellWolf
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView fotoWolf { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel textTableViewWolvesName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (fotoWolf != null) {
                fotoWolf.Dispose ();
                fotoWolf = null;
            }

            if (textTableViewWolvesName != null) {
                textTableViewWolvesName.Dispose ();
                textTableViewWolvesName = null;
            }
        }
    }
}