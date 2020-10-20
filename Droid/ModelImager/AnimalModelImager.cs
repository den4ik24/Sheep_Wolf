using System;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.Droid
{
    public static class AnimalModelImager
    {
        public static int GetAnimalImage(AnimalModel model, AnimalState state)
        {
            if (model is SheepModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Resource.Drawable.sheep;
                }
                if (state == AnimalState.DEAD)
                {
                    return Resource.Drawable.rip;
                }
            }

            if (model is WolfModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Resource.Drawable.wolf;
                }
                if (state == AnimalState.DEAD)
                {
                    return Resource.Drawable.wolf_rip;
                }
                if (state == AnimalState.KILLER)
                {
                    return Resource.Drawable.killer;
                }
            }

            if (model is HunterModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Resource.Drawable.hunter;
                }
                if (state == AnimalState.DEAD)
                {
                    return Resource.Drawable.hunter_rip;
                }
                if (state == AnimalState.KILLER)
                {
                    return Resource.Drawable.hunter_killer;
                }
            }

            if (model is DuckModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Resource.Drawable.duck;
                }
            }

            throw new Exception();
        }
    }
}
