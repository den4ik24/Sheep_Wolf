using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 0;
        List<AnimalClassIOS> animalNamesArray = new List<AnimalClassIOS>();
        List<string> animalsNameList = new List<string>();
        AnimalPickerModel picker;
        UIPickerView uiPicker;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            ButtonAddSheep.TouchUpInside += ButtonAddSheep_TouchUpInside;

            picker = new AnimalPickerModel(animalChoice);
            uiPicker = new UIPickerView();
            uiPicker.Model = picker;
            uiPicker.Model.Selected(uiPicker, 0, 0);
            animalChoice.InputView = uiPicker;
        }

        private void ButtonAddSheep_TouchUpInside(object sender, EventArgs e)
        {
            if (textNameOfSheep.Text == "")
            {
                var alertController = UIAlertController.Create
                    ("WARNING", "Введите имя животного", UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create
                    ("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alertController, true, null);
            }
            else
            {
                if (animalsNameList.Contains(textNameOfSheep.Text))
                {
                    var alertController = UIAlertController.Create
                    ("WARNING", "Животное с таким именем уже существует.Измените имя", UIAlertControllerStyle.Alert);
                    alertController.AddAction(UIAlertAction.Create
                        ("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(alertController, true, null);
                }
                else
                {
                    animalsNameList.Add(textNameOfSheep.Text);
                    RandAnimal();
                    listOfSheeps.Source = new TableSource(animalNamesArray, this);
                    listOfSheeps.ReloadData();
                    count++;
                    LabelNumberSheep.Text = count.ToString();
                    textNameOfSheep.Text = "";
                }
            }
        }

        public AnimalClassIOS RandAnimal()
        {
            AnimalClassIOS animal;

            if(picker.SelectedValue == Keys.SHEEP)
            {
                animal = new SheepClassIOS();
            }
            else
            {
                animal = new WolfClassIOS();
            }

            animal.Name = textNameOfSheep.Text;
            animalNamesArray.Add(animal);
            return animal;
        }
    }
}
