using System;
using Sheep_Wolf_NetStandardLibrary;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 0;
        AnimalPickerModel picker;
        UIPickerView uiPicker;
        BusinessLogic businessLogic = new BusinessLogic();
         
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
                AddRandomAnimal();
                listOfSheeps.Source = new TableSource(businessLogic.animalModelsArray, this);
                listOfSheeps.ReloadData();
                count++;
                LabelNumberAnimal.Text = count.ToString();
                textNameOfAnimals.Text = "";

            }
        }

        public void AddRandomAnimal()
        {
            var isSheep = picker.SelectedValue == AnimalType.SHEEP.ToString();
            if(businessLogic.AddAnimal(isSheep, textNameOfAnimals.Text))
            {
                var alertController = UIAlertController.Create
                ("WARNING", "Животное с таким именем уже существует.Измените имя", UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create
                    ("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alertController, true, null);
            }
            
        }
    }
}