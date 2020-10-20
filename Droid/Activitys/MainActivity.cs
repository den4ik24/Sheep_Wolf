using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Sheep_Wolf_NetStandardLibrary;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Sheep_Wolf.Droid
{
    [Activity(Label = "Circle of Life", Icon = "@mipmap/icon", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView _textViewNumbSheep;
        RecyclerView _listOfAnimals;
        EditText _textNameOfAnimal;
        Spinner _animalChoice;
        AnimalAdapter _adapter;
        V7Toolbar _myToolbar;
        IMenu menu;
        readonly IBusinessLogic businessLogic = new BusinessLogic();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            _myToolbar = FindViewById<V7Toolbar>(Resource.Id.my_toolbar);
            _textViewNumbSheep = FindViewById<TextView>(Resource.Id.textViewNumbSheep);
            _listOfAnimals = FindViewById<RecyclerView>(Resource.Id.listOfAnimals);
            _textNameOfAnimal = FindViewById<EditText>(Resource.Id.textNameOfAnimal);
            _animalChoice = FindViewById<Spinner>(Resource.Id.animalChoice);
            
            SetSupportActionBar(_myToolbar);
            var layoutManager = new LinearLayoutManager(this);
            var adapterSpinner = ArrayAdapter<string>.CreateFromResource(this, Resource.Array.type_animal, Android.Resource.Layout.SimpleSpinnerItem);
            _adapter = new AnimalAdapter();
            
            _listOfAnimals.SetLayoutManager(layoutManager);
            businessLogic.GetListAnimals();
            ChangeAnimalCount();
            _adapter.animalModelsArray = businessLogic.AnimalModel();
            _adapter.ItemClick += ListOfAnimals_ItemClick;
            _listOfAnimals.SetAdapter(_adapter);
            _animalChoice.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(AnimalChoice_ItemSelected);
            adapterSpinner.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _animalChoice.Adapter = adapterSpinner;
            _animalChoice.SetSelection(0);
            businessLogic.Notify += DisplayKillMessage;
            businessLogic.DataChanged += DataSetChanged;

            _textNameOfAnimal.TextChanged += TextNameOfAnimal_TextChanged;
        }

        private void AnimalChoice_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            _animalChoice = sender as Spinner;
            var resIcon = ContextCompat.GetDrawable(this, Resource.Drawable.animal_logo);
            var item = menu.FindItem(Resource.Id.addAnimals);
            if (_animalChoice.SelectedItemPosition is (int)AnimalType.DUCK ||
                _animalChoice.SelectedItemPosition is (int) AnimalType.HUNTER)
            {
                SetEnabledIconState(item, resIcon, true);
                _textNameOfAnimal.Enabled = false;
                //_textNameOfAnimal.Text = Keys.ENTERthePAW;
                _textNameOfAnimal.Visibility = ViewStates.Gone;
            }
            else
            {
                _textNameOfAnimal.Visibility = ViewStates.Visible;
                SetIconColorDisabled(item, resIcon);
                _textNameOfAnimal.Enabled = true;
                _textNameOfAnimal.Text = "";
            }
        }

        private void ListOfAnimals_ItemClick(object sender, int e)
        {
            var intent = new Intent(this, typeof(AnimalIDActivity));
            var animal = _adapter.ElementPosition(e);
            DataTransmission(animal, intent);
            StartActivity(intent);
        }

        public void DataTransmission(AnimalModel animal, Intent intent)
        {
            var type = businessLogic.TypeOfAnimal(animal);
            intent.PutExtra(Keys.TYPEofANIMAL, (int)type);
            intent.PutExtra(Keys.ANIMAL_ID, animal.Id);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            var resIcon = ContextCompat.GetDrawable(this, Resource.Drawable.animal_logo);
            var item = menu.FindItem(Resource.Id.addAnimals);
            SetIconColorDisabled(item, resIcon);
            this.menu = menu;
            return true;
        }
        
        private void TextNameOfAnimal_TextChanged(object sender, TextChangedEventArgs e)
        {
            var resIcon = ContextCompat.GetDrawable(this, Resource.Drawable.animal_logo);
            var item = menu.FindItem(Resource.Id.addAnimals);
            if (SheepAndWolfSelected())
            {
                SetIconColorDisabled(item, resIcon);
            }
            else
            {
                SetEnabledIconState(item, resIcon, true);
            }
        }

        public void SetEnabledIconState(IMenuItem item, Drawable resIcon, bool enabled)
        {
            item.SetIcon(resIcon);
            item.SetEnabled(enabled);
        }
        
        public void SetIconColorDisabled(IMenuItem item, Drawable resIcon)
        {
            resIcon.Mutate().SetColorFilter(Color.DarkGray, PorterDuff.Mode.SrcIn);
            SetEnabledIconState(item, resIcon, false);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        { 
            switch (item.ItemId)
            {
                case Resource.Id.addAnimals:
                    if (SheepAndWolfSelected())
                    {
                        var toast = Toast.MakeText(this, Keys.ENTERtheNAME, ToastLength.Short);
                        toast.SetGravity(GravityFlags.Center, 0, 0);
                        var toastContainer = (LinearLayout)toast.View;
                        toastContainer.SetBackgroundColor(Color.Transparent);
                        toast.Show();
                    }
                    else
                    {
                        DeleteKeyboard();
                        AddRandomAnimal();
                        _textNameOfAnimal.Text = "";
                    }
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public bool SheepAndWolfSelected()
        {
            var sw = string.IsNullOrEmpty(_textNameOfAnimal.Text) &&
               (_animalChoice.SelectedItemPosition is (int)AnimalType.SHEEP ||
                _animalChoice.SelectedItemPosition is (int)AnimalType.WOLF);
            return sw;
        }

        public void AddRandomAnimal()
        {
            var isSheep = _animalChoice.SelectedItemPosition;
            if (isSheep is (int)AnimalType.WOLF)
            {
                Toast.MakeText(this, Keys.ENTERtheWOLF, ToastLength.Short).Show();
            }
            else if (isSheep is (int)AnimalType.HUNTER)
            {
                Toast.MakeText(this, Keys.ENTERtheHUNTER, ToastLength.Short).Show();
            }

            if (businessLogic.AddAnimal(isSheep, _textNameOfAnimal.Text))
            {
                var toast = Toast.MakeText(this, Keys.REPEATtheNAME, ToastLength.Short);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                var toastContainer = (LinearLayout)toast.View;
                var imageToast = new ImageView(this);
                imageToast.SetImageResource(Resource.Drawable.INFORMATION);
                toastContainer.AddView(imageToast, 0);
                toastContainer.SetBackgroundColor(Color.Transparent);
                toast.Show();
            }
            else
            {
                ChangeAnimalCount();
                DeleteKeyboard();
            }
            _adapter.NotifyDataSetChanged();
        }

        public void ChangeAnimalCount()
        {
            _textViewNumbSheep.Text = businessLogic.AnimalModel().Count.ToString();
        }

        public void DisplayKillMessage(object sender, DataTransfer transferData)
        {
            if (transferData.TypeKiller == KillerAnnotation.HUNTER_KILL_WOLF)
            {
                ImageToast(transferData.Message, Resource.Drawable.hunter_kill_wolf);
            }
            else if (transferData.TypeKiller == KillerAnnotation.WOLF_EAT_HUNTER)
            {
                ImageToast(transferData.Message, Resource.Drawable.wolf_kill_hunter);
            }
            else if (transferData.TypeKiller == KillerAnnotation.WOLF_EAT_SHEEP)
            {
                ImageToast(transferData.Message, Resource.Drawable.wolf_kill);
            }
        }

        public void ImageToast(string message, int picture)
        {
            RunOnUiThread(() =>
            {
                var toast = Toast.MakeText(this, message, ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                var toastContainer = (LinearLayout)toast.View;
                var imageToast = new ImageView(this);
                toastContainer.AddView(imageToast, 0);
                imageToast.SetImageResource(picture);
                toastContainer.SetBackgroundColor(Color.Transparent);
                toast.Show();
            });
        }

        public void DeleteKeyboard()
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(_textNameOfAnimal.WindowToken, 0);
        }
        public void DataSetChanged(object sender, EventArgs e)
        {
            RunOnUiThread(() =>
            {
                _adapter.NotifyDataSetChanged();
            });
        }
    }
}