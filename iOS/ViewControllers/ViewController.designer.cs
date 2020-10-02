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
        UIKit.UITextField animalChoice { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem ButtonAddAnimal { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CircleOfLife { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LabelNumberAnimal { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView listOfSheeps { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textNameOfAnimals { get; set; }
        [Action ("ButtonAddAnimal_TouchUpInside:")]
        partial void ButtonAddAnimal_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (animalChoice != null) {
                animalChoice.Dispose ();
                animalChoice = null;
            }

            if (ButtonAddAnimal != null) {
                ButtonAddAnimal.Dispose ();
                ButtonAddAnimal = null;
            }

            if (CircleOfLife != null) {
                CircleOfLife.Dispose ();
                CircleOfLife = null;
            }

            if (LabelNumberAnimal != null) {
                LabelNumberAnimal.Dispose ();
                LabelNumberAnimal = null;
            }

            if (listOfSheeps != null) {
                listOfSheeps.Dispose ();
                listOfSheeps = null;
            }

            if (textNameOfAnimals != null) {
                textNameOfAnimals.Dispose ();
                textNameOfAnimals = null;
            }
        }
    }
}