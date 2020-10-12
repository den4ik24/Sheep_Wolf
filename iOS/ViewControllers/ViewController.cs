using System;
using System.Timers;
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
        Timer timer = new Timer(5000);
        int alfa;

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
            CircleOfLife.Clicked += CircleOfLife_Clicked;
            picker.ValueChanged += AnimalChoice_ItemSelected;
            businessLogic.DataChanged += DataSetChanged;
            businessLogic.Notify += DisplayKillMessage;
            toastView.Layer.BorderWidth = 1;
            toastView.Layer.BorderColor = UIColor.Gray.CGColor;
            toastView.Layer.CornerRadius = 30;
        }

        private void CircleOfLife_Clicked(object sender, EventArgs e)
        {
            string message = "";
            string picture = "INFORMATION.png";
            ImageToast(message, picture);
        }

        private void AnimalChoice_ItemSelected(object sender, EventArgs e)
        {
            if (picker.SelectedValue == AnimalType.DUCK.ToString() ||
               picker.SelectedValue == AnimalType.HUNTER.ToString())
            {
                ButtonAddAnimal.Enabled = true;
                textNameOfAnimals.Enabled = false;
                textNameOfAnimals.Text = Keys.ENTERthePAW;
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
                    ("WARNING", Keys.ENTERtheNAME, UIAlertControllerStyle.Alert);
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

            if (isSheep == AnimalType.WOLF.ToString())
            {
                ImageToast(Keys.ENTERtheWOLF, "wolf.png");
            }
            else if (isSheep == AnimalType.HUNTER.ToString())
            {
                ImageToast(Keys.ENTERtheHUNTER, "hunter_killer.png");
            }

            if (businessLogic.AddAnimal(picker.SelectedRow, textNameOfAnimals.Text))
            {
                var alertController = UIAlertController.Create
                ("WARNING", Keys.REPEATtheNAME, UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create
                    ("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alertController, true, null);
                ImageToast(Keys.REPEATtheNAME, "INFORMATION.png");
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
                string picture;
                if (transferData.TypeKiller == KillerAnnotation.HUNTER_KILL_WOLF)
                {
                    picture = "hunter_kill_wolf.png";
                ImageToast(transferData.Message, picture);
                }
                if (transferData.TypeKiller == KillerAnnotation.WOLF_EAT_HUNTER)
                {
                    picture = "wolf_kill_hunter.png";
                ImageToast(transferData.Message, picture);
                }
                if (transferData.TypeKiller == KillerAnnotation.WOLF_EAT_SHEEP)
                {
                    picture = "wolf_kill.png";
                ImageToast(transferData.Message, picture);
                }

            });

        }
        public void ImageToast(string message, string picture)
        {
            timer.Elapsed += (o, args) => { NullToast(); };
            timer.AutoReset = true;
            timer.Enabled = true;
            alfa = 1;
            Anime(alfa);
            toastIOS.Text = message;
            pictureToast.Image = UIImage.FromBundle(picture);
            
       
        }

        public void NullToast()
        {
            InvokeOnMainThread(() =>
            {
                alfa = 0;
                Anime(alfa);
                timer.Stop();
                timer.AutoReset = false;
                timer.Enabled = false;
            });
        }

        public void Anime(int alfa)
        {
            UIView.Animate(2, () => { toastView.Alpha = alfa; pictureToast.Alpha = alfa; });
            
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