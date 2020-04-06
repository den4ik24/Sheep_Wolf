using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Square.Picasso;

namespace Sheep_Wolf.Droid
{

    public class AnimalAdapter: BaseAdapter<AnimalClass>
    {
        private readonly List<AnimalClass> animalClassArray = new List<AnimalClass>();
        private readonly Context context;

        public void Add(AnimalClass S)
        {
            animalClassArray.Add(S);
        }

        public AnimalAdapter(Context context)
        {
            this.context = context;
        }

        public override AnimalClass this[int position]
        {
            get
            {
                return animalClassArray[position];
            }
        }

        public override int Count
        {
            get
            {
                return animalClassArray.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public AnimalClass ElementPosition(int position)
        {
            return animalClassArray[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if(view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.SheepCheckList, parent, false);
                var textview = view.FindViewById<TextView>(Resource.Id.textViewSheepsNameAdapter);
                view.Tag = new ViewHolder() { TextView = textview };
            }

            var fotoSheep = view.FindViewById<ImageView>(Resource.Id.fotoSheep);
            Picasso.With(context)
                .Load(animalClassArray[position].URL)
                .Into(fotoSheep);

            var animalName = view.FindViewById<TextView>(Resource.Id.textViewSheepAdapter);
            if (animalClassArray[position] is SheepClass)
            {
                animalName.Text = "SHEEP";
            }
            else
            {
                animalName.Text = "WOLF";
            }

            var holder = (ViewHolder)view.Tag;
            holder.TextView.Text = animalClassArray[position].Name;

            return view;
        }
    }

    public class ViewHolder : Java.Lang.Object
    {
        public TextView TextView;
    }
}
