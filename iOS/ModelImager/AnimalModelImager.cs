using System;
using Sheep_Wolf_NetStandardLibrary;

namespace Sheep_Wolf.iOS
{
    public static class AnimalModelImager
    {
        public static string GetAnimalImage(AnimalModel model, AnimalState state)
        {
            if (model is SheepModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Foto.SHEEP;
                }
                if (state == AnimalState.DEAD)
                {
                    return Foto.SHEEP_RIP;
                }
            }

            if (model is WolfModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Foto.WOLF;
                }
                if (state == AnimalState.DEAD)
                {
                    return Foto.WOLF_RIP;
                }
                if (state == AnimalState.KILLER)
                {
                    return Foto.WOLF_KILLER;
                }
            }

            if (model is HunterModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Foto.HUNTER;
                }
                if (state == AnimalState.DEAD)
                {
                    return Foto.HUNTER_RIP;
                }
                if (state == AnimalState.KILLER)
                {
                    return Foto.HUNTER_KILLER;
                }
            }

            if (model is DuckModel)
            {
                if (state == AnimalState.ALIVE)
                {
                    return Foto.DUCK;
                }
            }

            throw new Exception();
        }
    }
}
