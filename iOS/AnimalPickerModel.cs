using System;
using System.Collections.Generic;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class AnimalPickerModel : UIPickerViewModel
    {
        List<string> Animals;
        public EventHandler ValueChanged;
        public string SelectedValue;

        public AnimalPickerModel(List<string> Animals)
        {
            this.Animals = Animals;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Animals.Count; //чтобы указать PickerView, где он должен брать информацию, которую собирается отображать. 
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1; //чтобы указать количество ячеек, в которых будет размещаться информация в PickerView
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Animals[(int)row]; //чтобы отобразить информацию на самом PickerView
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var animals = Animals[(int)row];

            SelectedValue = animals;
            ValueChanged?.Invoke(null, null);
        }

    }
}
