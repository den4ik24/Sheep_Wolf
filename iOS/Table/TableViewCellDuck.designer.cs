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
    [Register ("TableViewCellDuck")]
    partial class TableViewCellDuck
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView fotoDuck { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel textTableViewDucksName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (fotoDuck != null) {
                fotoDuck.Dispose ();
                fotoDuck = null;
            }

            if (textTableViewDucksName != null) {
                textTableViewDucksName.Dispose ();
                textTableViewDucksName = null;
            }
        }
    }
}