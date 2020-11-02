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
        UIKit.UIImageView animalFoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageAnimal { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelAnimalName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameAnimalID { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView starsLayout { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView viewID { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel whoKillMe { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (animalFoto != null) {
                animalFoto.Dispose ();
                animalFoto = null;
            }

            if (ImageAnimal != null) {
                ImageAnimal.Dispose ();
                ImageAnimal = null;
            }

            if (labelAnimalName != null) {
                labelAnimalName.Dispose ();
                labelAnimalName = null;
            }

            if (labelName != null) {
                labelName.Dispose ();
                labelName = null;
            }

            if (NameAnimalID != null) {
                NameAnimalID.Dispose ();
                NameAnimalID = null;
            }

            if (starsLayout != null) {
                starsLayout.Dispose ();
                starsLayout = null;
            }

            if (viewID != null) {
                viewID.Dispose ();
                viewID = null;
            }

            if (whoKillMe != null) {
                whoKillMe.Dispose ();
                whoKillMe = null;
            }
        }
    }
}