using System;
using System.Collections.Generic;
using UIKit;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 0;
        List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        List<string> animalsNameList = new List<string>();
        AnimalPickerModel picker;
        UIPickerView uiPicker;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            ButtonAddAnimal.TouchUpInside += ButtonAddAnimal_TouchUpInside;

            picker = new AnimalPickerModel(animalChoice);
            uiPicker = new UIPickerView();
            uiPicker.Model = picker;
            uiPicker.Model.Selected(uiPicker, 0, 0);
            animalChoice.InputView = uiPicker;
        }

        private void ButtonAddAnimal_TouchUpInside(object sender, EventArgs e)
        {
            if (textNameOfAnimals.Text == "")
            {
                var alertController = UIAlertController.Create
                    ("WARNING", "Введите имя животного", UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create
                    ("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alertController, true, null);
            }
            else
            {
                if (animalsNameList.Contains(textNameOfAnimals.Text))
                {
                    var alertController = UIAlertController.Create
                    ("WARNING", "Животное с таким именем уже существует.Измените имя", UIAlertControllerStyle.Alert);
                    alertController.AddAction(UIAlertAction.Create
                        ("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(alertController, true, null);
                }
                else
                {
                    animalsNameList.Add(textNameOfAnimals.Text);
                    RandAnimal();
                    listOfSheeps.Source = new TableSource(animalModelsArray, this);
                    listOfSheeps.ReloadData();
                    count++;
                    LabelNumberAnimal.Text = count.ToString();
                    textNameOfAnimals.Text = "";
                }
            }
        }

        public AnimalModel RandAnimal()
        {
            AnimalModel animal;

            if(picker.SelectedValue == AnimalType.SHEEP.ToString())
            {
                animal = new SheepModel();
            }
            else
            {
                animal = new WolfModel();
            }

            animal.Name = textNameOfAnimals.Text;
            animalModelsArray.Add(animal);

            SheepAssignment.SheepIsDead(animal, animalModelsArray);
            return animal;
        }
    }
}
