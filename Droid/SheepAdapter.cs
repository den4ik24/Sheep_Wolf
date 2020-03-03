using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace Sheep_Wolf.Droid
{

    public class SheepAdapter: BaseAdapter<string>
    {
        private readonly List<string> sheepNamesArray = new List<string>();
        private Context context;

        public void Add(string S)
        {
            sheepNamesArray.Add(S);
        }

        public SheepAdapter(Context context)
        {
            
            this.context = context;
        }

        public override string this[int position]
        {
            get
            {
                return sheepNamesArray[position];
            }
        }

        public override int Count
        {
            get
            {
                return sheepNamesArray.Count;
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

                //var viewHolder = new ViewHolder();
                //viewHolder.textView = view.FindViewById<TextView>(Resource.Id.textViewSheepsName);
                //view.Tag = viewHolder;
                var textview = view.FindViewById<TextView>(Resource.Id.textViewSheepsName);
                view.Tag = new ViewHolder() { TextView = textview };

            }
            var holder = (ViewHolder)view.Tag;
            holder.TextView.Text = sheepNamesArray[position];

            return view;
        }
    }

    public class ViewHolder : Java.Lang.Object
    {
        public TextView TextView;

    }
}
