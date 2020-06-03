﻿using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Sheep_Wolf_NetStandardLibrary;
using Square.Picasso;

namespace Sheep_Wolf.Droid
{
    public class AnimalAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        readonly Context context;
        private const int Sheep_Class = 0;
        private const int Wolf_Class = 1;
        public List<AnimalModel> animalModelsArray;

        public AnimalAdapter(Context context)
        {
            this.context = context;
        }

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

        public override int ItemCount
        {
            get
            {
                return animalModelsArray.Count;
            }
        }

        public AnimalModel ElementPosition(int position)
        {
            return animalModelsArray[position];
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == Sheep_Class)
            {
                SheepViewHolder holderSheep;
                var viewSheep = LayoutInflater.From(context).Inflate(Resource.Layout.SheepCheckList, parent, false);
                holderSheep = new SheepViewHolder(viewSheep, OnClick);

                viewSheep.Tag = holderSheep;
                return holderSheep;
            }
            else
            {
                WolfViewHolder holderWolf;
                var viewWolf = LayoutInflater.From(context).Inflate(Resource.Layout.WolfCheckList, parent, false);
                holderWolf = new WolfViewHolder(viewWolf, OnClick);
                viewWolf.Tag = holderWolf;
                return holderWolf;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            int row = GetItemViewType(position);
            switch (row)
            {
                case Sheep_Class:
                    var holderSheep = holder as SheepViewHolder;
                    holderSheep.textSheep.Text = animalModelsArray[position].Name;
                    if (animalModelsArray[position].IsDead)
                    {
                        Picasso.Get()
                            .Load(Resource.Drawable.rip)
                            .Into(holderSheep.imageSheep);
                    }
                    else
                    {
                        Picasso.Get()
                               .Load(animalModelsArray[position].URL)
                               .Into(holderSheep.imageSheep);
                    }
                    break;

                case Wolf_Class:
                    var holderWolf = holder as WolfViewHolder;
                    holderWolf.textWolf.Text = animalModelsArray[position].Name;
                    Picasso.Get()
                               .Load(animalModelsArray[position].URL)
                               .Into(holderWolf.imageWolf);
                    break;
            }
        }

        private void OnClick(int obj)
        {
            ItemClick?.Invoke(this, obj);
        }
    }

    public class SheepViewHolder : RecyclerView.ViewHolder
    {
        public TextView textSheep;
        public ImageView imageSheep;
        public SheepViewHolder(View view, Action<int> listener) :base(view)
        {
            textSheep = view.FindViewById<TextView>(Resource.Id.textViewSheepsNameAdapter);
            imageSheep = view.FindViewById<ImageView>(Resource.Id.fotoSheep);
            view.Click+= (sender, e)=> listener(LayoutPosition);
        }
    }

    public class WolfViewHolder : RecyclerView.ViewHolder
    {
        public TextView textWolf;
        public ImageView imageWolf;
        public WolfViewHolder(View view, Action<int> listener) :base(view)
        {
            textWolf = view.FindViewById<TextView>(Resource.Id.textViewWolvesNameAdapter);
            imageWolf = view.FindViewById<ImageView>(Resource.Id.fotoWolf);
            view.Click += (sender, e) => listener(LayoutPosition);
        }
    }
}
