using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using Sheep_Wolf_NetStandardLibrary;
using System;

namespace Sheep_Wolf.Droid
{
    public class AnimalAdapter : BaseAdapter<AnimalModel>
    {
        readonly Context context;
        private const int Sheep_Class = 0;
        private const int Wolf_Class = 1;
        public List<AnimalModel> animalModelsArray;

        public AnimalAdapter(Context context)
        {
            this.context = context;
        }

        public override int ViewTypeCount => 2;

        public override int GetItemViewType(int position)
        {
            if(animalModelsArray[position] is SheepModel)
            {
                return Sheep_Class;
            }
            else
            {
                return Wolf_Class;
            }
        }

        public override AnimalModel this[int position]
        {
            get
            {
                return animalModelsArray[position];
            }
        }

        public override int Count
        {
            get
            {
                return animalModelsArray.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public AnimalModel ElementPosition(int position)
        {
            return animalModelsArray[position];
        }

                    
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            int row = GetItemViewType(position);
            
            switch (row)
            {
                case Sheep_Class:
                    SheepViewHolder holderSheep;
                    var viewSheep = convertView;
                    if (viewSheep == null)
                    {
                        viewSheep = LayoutInflater.From(context).Inflate(Resource.Layout.SheepCheckList, parent, false);
                        holderSheep = new SheepViewHolder(viewSheep);
                        viewSheep.Tag = holderSheep;
                    }
                    else
                    {
                        holderSheep = viewSheep.Tag as SheepViewHolder;
                    }
                    holderSheep.textSheep.Text = animalModelsArray[position].Name;

                    if (animalModelsArray[position].IsDead)
                    {
                        Picasso
                            .With(context)
                            .Load(Resource.Drawable.rip)
                            .Into(holderSheep.imageSheep);
                    }
                    else
                    {
                        Picasso.With(context)
                               .Load(animalModelsArray[position].URL)
                               .Into(holderSheep.imageSheep);
                    }
                    return viewSheep;

                case Wolf_Class:
                    WolfViewHolder holderWolf;
                    var viewWolf = convertView;
                    if (viewWolf == null)
                    {
                        viewWolf = LayoutInflater.From(context).Inflate(Resource.Layout.WolfCheckList, parent, false);
                        holderWolf = new WolfViewHolder(viewWolf);
                        viewWolf.Tag = holderWolf;
                    }
                    else
                    {
                        holderWolf = viewWolf.Tag as WolfViewHolder;
                    }
                    holderWolf.textWolf.Text = animalModelsArray[position].Name;
                        Picasso.With(context)
                               .Load(animalModelsArray[position].URL)
                               .Into(holderWolf.imageWolf);
                    
                    return viewWolf;
            }
            return convertView;
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
