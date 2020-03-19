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
    [Register ("CardIDViewController")]
    partial class CardIDViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageSheep { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelSheepName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView sheepFoto { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImageSheep != null) {
                ImageSheep.Dispose ();
                ImageSheep = null;
            }

            if (labelSheepName != null) {
                labelSheepName.Dispose ();
                labelSheepName = null;
            }

            if (sheepFoto != null) {
                sheepFoto.Dispose ();
                sheepFoto = null;
            }
        }
    }
}