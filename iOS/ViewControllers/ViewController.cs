using System;
using Sheep_Wolf_NetStandardLibrary;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public partial class ViewController : UIViewController
    {
        AnimalPickerModel picker;
        UIPickerView uiPicker;
        readonly IBusinessLogic businessLogic = new BusinessLogic();
        public ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ButtonAddAnimal.Clicked += ButtonAddAnimal_Clicked;
            textNameOfAnimals.EditingChanged += TextNameOfAnimals_EditingChanged;
            businessLogic.GetListAnimals();
            listOfSheeps.Source = new TableSource(businessLogic.AnimalModel(), this);
            CountAnimal();
            picker = new AnimalPickerModel(animalChoice);
            uiPicker = new UIPickerView();
            uiPicker.Model = picker;
            uiPicker.Model.Selected(uiPicker, 0, 0);
            animalChoice.InputView = uiPicker;
            CircleOfLife.Image = CircleOfLife.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            picker.ValueChanged += AnimalChoice_ItemSelected;
            businessLogic.DataChanged += DataSetChanged;
            businessLogic.Notify += DisplayKillMessage;
        }

        private void AnimalChoice_ItemSelected(object sender, EventArgs e)
        {
            if (picker.SelectedValue == AnimalType.DUCK.ToString() ||
               picker.SelectedValue == AnimalType.HUNTER.ToString())
            {
                ButtonAddAnimal.Enabled = true;
                textNameOfAnimals.Enabled = false;
                textNameOfAnimals.Text = "Жми ЛАПКУ и добавляй без ввода имени";
            }
            else
            {
                ButtonAddAnimal.Enabled = false;
                textNameOfAnimals.Enabled = true;
                textNameOfAnimals.Text = "";
            }
        }

        private void TextNameOfAnimals_EditingChanged(object sender, EventArgs e)
        {
            if(SheepAndWolfSelected())
            {
                ButtonAddAnimal.Enabled = false;
            }
            else
            {
                ButtonAddAnimal.Enabled = true;
            }
        }

        public bool SheepAndWolfSelected()
        {
            var sw = string.IsNullOrEmpty(textNameOfAnimals.Text) &&
                (picker.SelectedValue == AnimalType.SHEEP.ToString() ||
                 picker.SelectedValue == AnimalType.WOLF.ToString());
            return sw;
        }

        private void ButtonAddAnimal_Clicked(object sender, EventArgs e)
        {
            if (SheepAndWolfSelected())
            {
                var alertController = UIAlertController.Create
                    ("WARNING", "Введите имя животного", UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create
                    ("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alertController, true, null);
            }
            else
            {
                DeleteKeyboard();
                AddRandomAnimal();
                textNameOfAnimals.Text = "";
                ButtonAddAnimal.Enabled = true;
            }
        }

        public void AddRandomAnimal()
        {
            var isSheep = picker.SelectedValue;
            if(businessLogic.AddAnimal(picker.SelectedRow, textNameOfAnimals.Text))
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
                DeleteKeyboard();
            }
            listOfSheeps.ReloadData();
        }

        public void DeleteKeyboard()
        {
            View.EndEditing(true);
        }

        public void CountAnimal()
        {
            LabelNumberAnimal.Text = businessLogic.AnimalModel().Count.ToString();
        }

        public void DisplayKillMessage(object sender, DataTransfer transferData)
        {
            InvokeOnMainThread(() =>
            {
                if (transferData.TypeKiller == KillerAnnotation.HUNTER_KILL_WOLF)
            {
                pictureToast.Image = UIImage.FromBundle("hunter_kill_wolf.png");
                ImageToast(transferData.Message);
            }
            else if (transferData.TypeKiller == KillerAnnotation.WOLF_EAT_HUNTER)
            {
                pictureToast.Image = UIImage.FromBundle("wolf_kill_hunter.png");
                ImageToast(transferData.Message);
            }
            else if (transferData.TypeKiller == KillerAnnotation.WOLF_EAT_SHEEP)
            {
                pictureToast.Image = UIImage.FromBundle("wolf_kill.png");
                ImageToast(transferData.Message);
            }
            });

        }
        public void ImageToast(string message)
        {
            toastIOS.BackgroundColor = UIColor.White;
            toastIOS.Text = message;
        }

        public void DataSetChanged(object sender, EventArgs e)
        {
            InvokeOnMainThread(() =>
            {
                listOfSheeps.ReloadData();
            });
        }
    }
}