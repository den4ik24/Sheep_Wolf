using System;
using System.Collections.Generic;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 0;
        List<AnimalClassIOS> animalModelsArray = new List<AnimalClassIOS>();
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

            animal.Name = textNameOfAnimals.Text;
            animalModelsArray.Add(animal);

            if(animal is WolfClassIOS)
            {
                for(var i = animalModelsArray.Count - 1; i >= 0; --i)
                {
                    var item = animalModelsArray[i];
                    if(item is SheepClassIOS && !item.IsDead)
                    {
                        item.IsDead = true;
                        item.Killer = animal.Name;
                        animal.Killer = item.Name;
                        break;
                    }
                }
            }
            return animal;
        }
    }
}
