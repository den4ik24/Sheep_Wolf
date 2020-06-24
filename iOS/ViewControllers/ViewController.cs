using System;
using Sheep_Wolf_NetStandardLibrary;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        AnimalPickerModel picker;
        UIPickerView uiPicker;
        IBusinessLogic businessLogic = new BusinessLogic();
         
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
          
            ButtonAddAnimal.Clicked += ButtonAddAnimal_TouchUpInside;
            businessLogic.GetListAnimals();
            CountAnimal();
            listOfSheeps.Source = new TableSource(businessLogic.AnimalModel(), this);
            picker = new AnimalPickerModel(animalChoice);
            uiPicker = new UIPickerView();
            uiPicker.Model = picker;
            uiPicker.Model.Selected(uiPicker, 0, 0);
            animalChoice.InputView = uiPicker;
            textNameOfAnimals.EditingChanged += TextNameOfAnimals_EditingChanged;
        }

        private void TextNameOfAnimals_EditingChanged(object sender, EventArgs e)
        {
            if(textNameOfAnimals.Text != "")
            {
                ButtonAddAnimal.Enabled = true;
            }
            else
            {
                ButtonAddAnimal.Enabled = false;
            }
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
                textNameOfAnimals.Text = "";
                ButtonAddAnimal.Enabled = false;
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
            else
            {
                CountAnimal();
                listOfSheeps.ReloadData();
            }
        }

        public void CountAnimal()
        {
            LabelNumberAnimal.Text = businessLogic.AnimalModel().Count.ToString();
        }
    }
}