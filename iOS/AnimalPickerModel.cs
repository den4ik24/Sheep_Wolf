using System;
using System.Collections.Generic;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class AnimalPickerModel : UIPickerViewModel
    {
        public string[] Animals = new string[]
        {
            "SHEEP", "WOLF"
        };
        public EventHandler ValueChanged;
        public string SelectedValue;
        private UITextField animalChoice;
        
        public AnimalPickerModel(UITextField animalChoice)
        {
            this.animalChoice = animalChoice;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Animals.Length; //чтобы указать PickerView, где он должен брать информацию, которую собирается отображать. 
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1; //чтобы указать количество ячеек, в которых будет размещаться информация в PickerView
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Animals[row].ToString(); //чтобы отобразить информацию на самом PickerView
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var animals = Animals[(int)row];
            animalChoice.Text = Animals[pickerView.SelectedRowInComponent(0)];
            
            SelectedValue = animals;
            ValueChanged?.Invoke(null, null);
        }

    }
}
