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
    [Register ("MyViewController")]
    partial class MyViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView textViewSheep { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView textViewSheepsName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (textViewSheep != null) {
                textViewSheep.Dispose ();
                textViewSheep = null;
            }

            if (textViewSheepsName != null) {
                textViewSheepsName.Dispose ();
                textViewSheepsName = null;
            }
        }
    }
}