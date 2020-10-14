using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Sheep_Wolf_NetStandardLibrary
{
    public interface IBusinessLogic
    {
        List<AnimalModel> AnimalModel();
        bool AddAnimal(int iS, string aN);
        void GetListAnimals();
        AnimalType TypeOfAnimal(AnimalModel animal);
        AnimalModel GetAnimal(string animalID, int typeOfAnimal);
        AnimalState GetAnimalState(AnimalModel animal);
        event EventHandler<DataTransfer> Notify;
        event EventHandler DataChanged;
    }

    public class BusinessLogic : IBusinessLogic
    {
        IDataBase dataBase = new DataBase();
        public List<AnimalModel> animalModelsArray = new List<AnimalModel>();
        int duckCount;
        public event EventHandler<DataTransfer> Notify;
        public event EventHandler DataChanged;
        Timer aTimer = new Timer();

        public List<AnimalModel> AnimalModel()
        {
            return animalModelsArray;
        }

        public bool AddAnimal(int isSheep, string animalName)
        {
            var animal = ChoiceAnimal(isSheep);
            if (isSheep is (int)AnimalType.DUCK ||
                isSheep is (int)AnimalType.SHEEP ||
                isSheep is (int)AnimalType.WOLF)
            {
                var repeatAnimal = animalModelsArray.Where(a => a.Name == animalName);
                if (repeatAnimal.Any())
                {
                    return true;
                }
                else
                {
                    if (animal is DuckModel)
                    {
                        animal.Name = $"Duck_{++duckCount}";
                        animalName = animal.Name;
                    }
                    ActionWithCreatures(animalName, animal);
                    return false;
                }
            }

            else
            {
                if (animal.IsDead == false && aTimer.Enabled == false)
                {
                    StartTimer(animal);
                }

                animalName = animal.Name;
                ActionWithCreatures(animalName, animal);
                return false;
            }
        }

        public AnimalModel ChoiceAnimal(int isSheep)
        {
            var type = (AnimalType)isSheep;
            AnimalModel animal;
            switch (type)
            {
                case AnimalType.SHEEP:
                    animal = SheepModel.GetSheep();
                    return animal;
                case AnimalType.WOLF:
                    animal = WolfModel.GetWolf();
                    return animal;
                case AnimalType.DUCK:
                    animal = DuckModel.GetDuck();
                    return animal;
                case AnimalType.HUNTER:
                    animal = HunterModel.GetHunter();
                    return animal;
                default: break;
            }
            return null;
        }

        public void ActionWithCreatures(string animalName, AnimalModel animal)
        {
            AssignAnimal(animalName, animal);
            AnimalKiller(animal);
            dataBase.Insert(animal);
        }

        public void AssignAnimal(string animalName, AnimalModel animal)
        {
            animal.Order = animalModelsArray.Count;
            animal.Name = animalName;
            animalModelsArray.Add(animal);
        }

        public void AnimalKiller(AnimalModel animal)
        {
            double wolfLiveCount = animalModelsArray.Count(a => a is WolfModel && !a.IsDead);
            double hunterLiveCount = animalModelsArray.Count(a => a is HunterModel && !a.IsDead);
            
            if (animal is WolfModel)
            {
                for (var i = animalModelsArray.Count - 1; i >= 0; --i)
                {
                    var item = animalModelsArray[i];
                    //волки жрут овцу
                    if (item is SheepModel && !item.IsDead)
                    {
                        WhoKilledWho(item, animal);
                        var dataTransfer = new DataTransfer
                        {
                            Message = $"Волк {animal.Name} сожрал овцу {item.Name}",
                            TypeKiller = KillerAnnotation.WOLF_EAT_SHEEP
                        };
                        fillPrey(animal, item);
                        Notify?.Invoke(this, dataTransfer);
                        DataChanged?.Invoke(this, EventArgs.Empty);

                        for (int k = animalModelsArray.Count - 1; k >= 0; --k)
                        {
                            var hunt = animalModelsArray[k];
                            if (hunt is HunterModel && !hunt.IsDead)
                            {
                                DuckFlyAway();
                                Timer _timer = new Timer(5000);
                                _timer.Elapsed += (o, args) => { WhoKilledWho(animal, hunt); };
                                _timer.AutoReset = true;
                                _timer.Enabled = true;
                                Console.WriteLine("завалили съевшего овцу");
                                var dataTrans = new DataTransfer
                                {
                                    Message = $"Охотник {hunt.Name} завалил волка {animal.Name}",
                                    TypeKiller = KillerAnnotation.HUNTER_KILL_WOLF
                                };
                                fillPrey(hunt, animal);
                                Notify?.Invoke(this, dataTrans);
                                DataChanged?.Invoke(this, EventArgs.Empty);
                                dataBase.Update(animal);
                                _timer.Stop();
                                _timer.Dispose();
                                break;
                            }
                        }

                        break;
                    }
                    //волки жрут охотника
                    else if (item is HunterModel && !item.IsDead)
                    {
                        if (hunterLiveCount <= 1)
                        {
                            StopTimer();
                        }
                        WhoKilledWho(item, animal);
                        DuckFlyAway();
                        var dataTransfer = new DataTransfer
                        {
                            Message = $"Волк {animal.Name} разодрал охотника {item.Name}",
                            TypeKiller = KillerAnnotation.WOLF_EAT_HUNTER
                        };
                        Notify?.Invoke(this, dataTransfer);
                        DataChanged?.Invoke(this, EventArgs.Empty);
                        fillPrey(animal, item);
                        break;
                    }
                }
            }

            if (animal is HunterModel)
            {
                for (var i = animalModelsArray.Count - 1; i >= 0; --i)
                {
                    var item = animalModelsArray[i];
                    //охотник валит волка
                    if (item is WolfModel && !item.IsDead)
                    {
                        if (hunterLiveCount <= 1)
                        {
                            StopTimer();
                        }
                        WhoKilledWho(item, animal);
                        DuckFlyAway();
                        Console.WriteLine("охотник валит волка");
                        var dataTransfer = new DataTransfer
                        {
                            Message = $"Охотник {animal.Name} завалил волка {item.Name}",
                            TypeKiller = KillerAnnotation.HUNTER_KILL_WOLF
                        };
                        Notify?.Invoke(this, dataTransfer);
                        DataChanged?.Invoke(this, EventArgs.Empty);
                        dataBase.Update(animal);
                        fillPrey(animal, item);

                        if (wolfLiveCount/2 > hunterLiveCount)
                        {
                            animal.IsDead = true;
                            animal.WhoKilledMe = item.Name;
                            item.Killer = animal.Name;
                            fillPrey(item, animal);
                            dataBase.Update(animal);
                            dataBase.Update(item);
                            DataChanged?.Invoke(this, EventArgs.Empty);
                            StopTimer();
                        }
                        break;
                    }
                }
            }
        }

        public void GetListAnimals()
        {
            dataBase.SelectTableID();
            animalModelsArray.AddRange(dataBase.SelectTable());
            animalModelsArray = animalModelsArray.OrderBy(a => a.Order).ToList();
        }

        public AnimalType TypeOfAnimal(AnimalModel animal)
        {
            AnimalType type;
            if (animal is SheepModel)
            {
                type = AnimalType.SHEEP;
            }
            else if (animal is WolfModel)
            {
                type = AnimalType.WOLF;
            }
            else if (animal is DuckModel)
            {
                type = AnimalType.DUCK;
            }
            else
            {
                type = AnimalType.HUNTER;
            }
            return type;
        }

        public AnimalModel GetAnimal(string animalID, int typeOfAnimal)
        {
            var type = (AnimalType)typeOfAnimal;
            switch (type)
            {
                case AnimalType.SHEEP:
                    return dataBase.GetItem<SheepModel>(animalID);
                case AnimalType.DUCK:
                    return dataBase.GetItem<DuckModel>(animalID);
                case AnimalType.WOLF:
                    return dataBase.GetItem<WolfModel>(animalID);
                case AnimalType.HUNTER:
                    return dataBase.GetItem<HunterModel>(animalID);
                default:
                    break;
            }
            return null;
        }

        public void WhoKilledWho(AnimalModel sacrifice, AnimalModel killer)
        {
            sacrifice.IsDead = true;
            sacrifice.WhoKilledMe = killer.Name;
            killer.Killer = sacrifice.Name;
            dataBase.Update(sacrifice);
            dataBase.Update(killer);
        }

        public AnimalState GetAnimalState(AnimalModel animal)
        {
            if (animal.IsDead)
            {
                return AnimalState.DEAD;
            }

            if(!animal.IsDead && animal.Killer == null)
            {
                return AnimalState.ALIVE;
            }

            if(animal.Killer != null)
            {
                return AnimalState.KILLER;
            }
            throw new Exception();
        }

        public void DuckFlyAway()
        {
            animalModelsArray.RemoveAll(a => a is DuckModel);
            dataBase.Delete<DuckModel>();
            duckCount = 0;
        }

        public void fillPrey(AnimalModel kilerID, AnimalModel victimID)
        {
            var prey = new Prey();
            prey.KillerId = kilerID.Id;
            prey.VictimId = victimID.Id;
            dataBase.InsertID(prey);
        }

        public void StartTimer(AnimalModel animal)
        {
            if (aTimer.Enabled == false)
            {
                aTimer.Start();
                aTimer.Interval = 5000;
                aTimer.Elapsed += (o, args) => { AnimalKiller(animal); };
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
            }
        }

        public void StopTimer()
        {
            aTimer.AutoReset = false;
            aTimer.Enabled = false;
        }
    }
    public class DataTransfer : EventArgs
    {
        public string Message { get; set; }
        public KillerAnnotation TypeKiller { get; set; }
    }
    public enum KillerAnnotation
    {
        WOLF_EAT_SHEEP,
        WOLF_EAT_HUNTER,
        HUNTER_KILL_WOLF
    }
    public enum AnimalState
    {
        DEAD,
        ALIVE,
        KILLER
    }
}
