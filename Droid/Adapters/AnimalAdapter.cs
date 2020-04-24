using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Square.Picasso;

namespace Sheep_Wolf.Droid
{
    public class AnimalAdapter : BaseAdapter<AnimalModel>
    {
        readonly List<AnimalModel> animalClassArray = new List<AnimalModel>();
        readonly Context context;
        private const int Sheep_Class = 0;
        private const int Wolf_Class = 1;

        public void Add(AnimalModel animal)
        {
            animalClassArray.Add(animal);

            if(animal is WolfModel)
            {
                for (var i = animalClassArray.Count - 1; i >= 0; --i)
                {
                    var item = animalClassArray[i];
                    if (item is SheepModel && !item.IsDead)
                    {
                        item.IsDead = true;
                        item.Killer = animal.Name;
                        animal.Killer = item.Name;
                        break;
                    }
                }
            }
        }

        public AnimalAdapter(Context context)
        {
            this.context = context;
        }

        public override int ViewTypeCount => 2;

        public override int GetItemViewType(int position)
        {
            if(animalClassArray[position] is SheepModel)
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

        public AnimalModel ElementPosition(int position)
        {
            return animalClassArray[position];
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
                    holderSheep.textSheep.Text = animalClassArray[position].Name;

                    if (animalClassArray[position].IsDead)
                    {
                        Picasso
                            .With(context)
                            .Load(Resource.Drawable.rip)
                            .Into(holderSheep.imageSheep);
                    }
                    else
                    {
                        Picasso.With(context)
                               .Load(animalClassArray[position].URL)
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
                    holderWolf.textWolf.Text = animalClassArray[position].Name;
                        Picasso.With(context)
                               .Load(animalClassArray[position].URL)
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
