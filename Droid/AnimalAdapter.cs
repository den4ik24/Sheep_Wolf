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
            if (animalClassArray[position] is SheepClass)
            {
                SheepViewHolder holderSheep;
                var view = convertView;
                if (view == null)
                {
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.SheepCheckList, parent, false);
                    holderSheep = new SheepViewHolder(view);
                    view.Tag = holderSheep;
                }
                else
                {
                    holderSheep = (SheepViewHolder)view.Tag;
                }
                holderSheep.textSheep.Text = animalClassArray[position].Name;
                Picasso.With(context).Load(animalClassArray[position].URL).Into(holderSheep.imageSheep);
                return view;
            }
            else
            {
                WolfViewHolder holderWolf;
                var view = convertView;
                if (view == null)
                {
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.WolfCheckList, parent, false);
                    holderWolf = new WolfViewHolder(view);
                    view.Tag = holderWolf;
                }
                else
                {
                    holderWolf = (WolfViewHolder)view.Tag;
                }
                holderWolf.textWolf.Text = animalClassArray[position].Name;
                Picasso.With(context).Load(animalClassArray[position].URL).Into(holderWolf.imageWolf);
                return view;
            }

        }
    }

    public class SheepViewHolder : Java.Lang.Object
    {
        public TextView textSheep;
        public ImageView imageSheep;
        public SheepViewHolder(View view)
        {
            textSheep = view.FindViewById<TextView>(Resource.Id.textViewSheepsNameAdapter);
            imageSheep = view.FindViewById<ImageView>(Resource.Id.fotoSheep);
        }
    }

    public class WolfViewHolder : Java.Lang.Object
    {
        public TextView textWolf;
        public ImageView imageWolf;
        public WolfViewHolder(View view)
        {
            textWolf = view.FindViewById<TextView>(Resource.Id.textViewWolvesNameAdapter);
            imageWolf = view.FindViewById<ImageView>(Resource.Id.fotoWolf);
        }
    }
}
