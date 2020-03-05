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
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonAddSheep { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelNumberSheep { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView listOfSheeps { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textNameOfSheep { get; set; }


        [Action ("ButtonAddSheep_TouchUpInside:")]
        partial void ButtonAddSheep_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ButtonAddSheep != null) {
                ButtonAddSheep.Dispose ();
                ButtonAddSheep = null;
            }

            if (LabelNumberSheep != null) {
                LabelNumberSheep.Dispose ();
                LabelNumberSheep = null;
            }

            if (listOfSheeps != null) {
                listOfSheeps.Dispose ();
                listOfSheeps = null;
            }

            if (textNameOfSheep != null) {
                textNameOfSheep.Dispose ();
                textNameOfSheep = null;
            }
        }
    }
}