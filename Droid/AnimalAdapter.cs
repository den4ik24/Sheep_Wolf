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
                var view = convertView;
                if (view == null)
                {
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.SheepCheckList, parent, false);
                    //var textviewSheep = view.FindViewById<TextView>(Resource.Id.textViewSheepsNameAdapter);
                    //var fotoSheep = view.FindViewById<ImageView>(Resource.Id.fotoSheep);
                    view.Tag = new SheepViewHolder(view);
                    //{
                    //    textSheep = textviewSheep,
                    //    imageSheep = fotoSheep
                    //};
                }
                var holder = (SheepViewHolder)view.Tag;
                holder.textSheep.Text = animalClassArray[position].Name;
                Picasso.With(context).Load(animalClassArray[position].URL).Into(holder.imageSheep);
                return view;
            }
            //if (animalClassArray[position] is WolfClass)
            else
            {
                var view = convertView;
                if (view == null)
                {
                    view = LayoutInflater.From(context).Inflate(Resource.Layout.WolfCheckList, parent, false);
                    //var textviewWolf = view.FindViewById<TextView>(Resource.Id.textViewWolvesNameAdapter);
                    //var fotoWolf = view.FindViewById<ImageView>(Resource.Id.fotoWolf);
                    view.Tag = new WolfViewHolder(view);
                    //{
                    //    textWolf = textviewWolf,
                    //    imageWolf = fotoWolf
                    //};
                }
                var holder = (WolfViewHolder)view.Tag;
                holder.textWolf.Text = animalClassArray[position].Name;
                Picasso.With(context).Load(animalClassArray[position].URL).Into(holder.imageWolf);
                return view;
            }
            //return convertView;
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
