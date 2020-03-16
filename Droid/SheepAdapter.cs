using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace Sheep_Wolf.Droid
{

    public class SheepAdapter: BaseAdapter<SheepClass>
    {
        private readonly List<SheepClass> sheepClassArray = new List<SheepClass>();
        private readonly Context context;

        public void Add(SheepClass S)
        {
            sheepClassArray.Add(S);
            
        }

        public SheepAdapter(Context contextC)
        {
            
            context = contextC;
        }

        public override SheepClass this[int position]
        {
            get
            {
                return sheepClassArray[position];
            }
        }

        public override int Count
        {
            get
            {
                return sheepClassArray.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if(view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.SheepCheckList, parent, false);

                var textview = view.FindViewById<TextView>(Resource.Id.textViewSheepsName);
                //image

                view.Tag = new ViewHolder() { TextView = textview };

            }
            var holder = (ViewHolder)view.Tag;

            holder.TextView.Text = sheepClassArray[position].Name;

            return view;
        }
    }

    public class ViewHolder : Java.Lang.Object
    {
        public TextView TextView;

    }
}
