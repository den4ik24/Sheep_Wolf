using System;
using System.Collections.Generic;
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
        private const int Sheep_Class = 0;
        private const int Wolf_Class = 1;
        private const int Duck_Class = 2;
        private const int Hunter_Class = 3;
        public List<AnimalModel> animalModelsArray;

        public override int GetItemViewType(int position)
        {
            if(animalModelsArray[position] is SheepModel)
            {
                return Sheep_Class;
            }
            else if (animalModelsArray[position] is WolfModel)
            {
                return Wolf_Class;
            }
            else if (animalModelsArray[position] is DuckModel)
            {
                return Duck_Class;
            }
            else
            {
                return Hunter_Class;
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
            switch (viewType)
            {
                case Sheep_Class:
                    SheepViewHolder holderSheep;
                    var viewSheep = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SheepCheckList, parent, false);
                    holderSheep = new SheepViewHolder(viewSheep, OnClick);
                    viewSheep.Tag = holderSheep;
                    return holderSheep;
                case Wolf_Class:
                    WolfViewHolder holderWolf;
                    var viewWolf = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.WolfCheckList, parent, false);
                    holderWolf = new WolfViewHolder(viewWolf, OnClick);
                    viewWolf.Tag = holderWolf;
                    return holderWolf;
                case Duck_Class:
                    DuckViewHolder holderDuck;
                    var viewDuck = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.DuckCheckList, parent, false);
                    holderDuck = new DuckViewHolder(viewDuck, OnClick);
                    viewDuck.Tag = holderDuck;
                    return holderDuck;
                case Hunter_Class:
                    HunterViewHolder holderHunter;
                    var viewHunter = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.HunterCheckList, parent, false);
                    holderHunter = new HunterViewHolder(viewHunter, OnClick);
                    viewHunter.Tag = holderHunter;
                    return holderHunter;
            }
            return (RecyclerView.ViewHolder)viewType;
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

                case Duck_Class:
                    var holderDuck = holder as DuckViewHolder;
                    holderDuck.textDuck.Text = animalModelsArray[position].Name;
                    Picasso.Get()
                               .Load(animalModelsArray[position].URL)
                               .Into(holderDuck.imageDuck);
                    break;

                case Wolf_Class:
                    var holderWolf = holder as WolfViewHolder;
                    holderWolf.textWolf.Text = animalModelsArray[position].Name;
                    Picasso.Get()
                               .Load(animalModelsArray[position].URL)
                               .Into(holderWolf.imageWolf);
                    break;

                case Hunter_Class:
                    var holderHunter = holder as HunterViewHolder;
                    holderHunter.textHunter.Text = animalModelsArray[position].Name;
                    Picasso.Get()
                               .Load(animalModelsArray[position].URL)
                               .Into(holderHunter.imageHunter);
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

    public class DuckViewHolder : RecyclerView.ViewHolder
    {
        public TextView textDuck;
        public ImageView imageDuck;
        public DuckViewHolder(View view, Action<int> listener) : base(view)
        {
            textDuck = view.FindViewById<TextView>(Resource.Id.textViewDucksNameAdapter);
            imageDuck = view.FindViewById<ImageView>(Resource.Id.fotoDuck);
            view.Click += (sender, e) => listener(LayoutPosition);
        }
    }

    public class HunterViewHolder : RecyclerView.ViewHolder
    {
        public TextView textHunter;
        public ImageView imageHunter;
        public HunterViewHolder(View view, Action<int> listener) : base(view)
        {
            textHunter = view.FindViewById<TextView>(Resource.Id.textViewHunterNameAdapter);
            imageHunter = view.FindViewById<ImageView>(Resource.Id.fotoHunter);
            view.Click += (sender, e) => listener(LayoutPosition);
        }
    }
}
