using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Sheep_Wolf_NetStandardLibrary
{
    public interface IBusinessLogic
    {
        List<AnimalModel> GetAnimalModel();
        bool AddAnimal(int iS, string aN);
        void GetListAnimals();
        AnimalType TypeOfAnimal(AnimalModel animal);
        AnimalModel GetAnimal(string animalID, int typeOfAnimal);
        AnimalState GetAnimalState(AnimalModel animal);
        event EventHandler<DataTransferEventArgs> Notify;
        event EventHandler <TransferModelsEventArgs> DataChanged;
        string NameofKiller(AnimalModel animal);
        string TextKill(AnimalModel animal);
    }

    public class BusinessLogic : IBusinessLogic
    {
        IDataBase _dataBase = new DataBase();
        int duckCount;
        public event EventHandler<DataTransferEventArgs> Notify;
        public event EventHandler<TransferModelsEventArgs> DataChanged;
        readonly Timer _aTimer = new Timer();
        readonly Prey _prey = new Prey();

        public List<AnimalModel> GetAnimalModel()
        {
            return _dataBase.GetAnimalModels();
        }

        public bool AddAnimal(int choiceSelectedAnimal, string animalName)
        {
            var animal = ChoiceAnimal(choiceSelectedAnimal);
            switch (choiceSelectedAnimal)
            {
                case (int)AnimalType.SHEEP:
                    if (_dataBase.nameVerification<SheepModel>(animalName))
                    {
                        return true;
                    }
                    ActionWithCreatures(animalName, animal);
                    return false;
                case (int)AnimalType.WOLF:
                    if (_dataBase.nameVerification<WolfModel>(animalName))
                    {
                        return true;
                    }
                    ActionWithCreatures(animalName, animal);
                    return false;
                case (int)AnimalType.DUCK:
                    animal.Name = $"Duck_{++duckCount}";
                    animalName = animal.Name;
                    ActionWithCreatures(animalName, animal);
                    return false;
                case (int)AnimalType.HUNTER:
                    if (animal.IsDead == false && _aTimer.Enabled == false)
                    {
                        StartTimer(animal);
                    }
                    animalName = animal.Name;
                    ActionWithCreatures(animalName, animal);
                    return false;
                default: break;
            }
            throw new Exception();
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
            _dataBase.Insert(animal);
        }

        public void AssignAnimal(string animalName, AnimalModel animal)
        {

            var countSheep = _dataBase.countAnimal<SheepModel>();
            var countWolf = _dataBase.countAnimal<WolfModel>();
            var countDuck = _dataBase.countAnimal<DuckModel>();
            var countHunter = _dataBase.countAnimal<HunterModel>();
            int[] nums = { countSheep, countWolf, countDuck, countHunter };
            int allCount = nums.Max() + 1;
            animal.Order += allCount;
            animal.Name = animalName;
        }

        public void AnimalKiller(AnimalModel animal)
        {
            Console.WriteLine("отработка AnimalKiller");
            switch (animal)
            {
                case WolfModel:
                    WolfIsKiller(animal);
                    return;

                case HunterModel:
                    HunterIsKiller(animal);
                    return;
            }
        }
        public void WolfIsKiller(AnimalModel animal)
        {
            var allCount = _dataBase.AnimalModelCount<AnimalModel>();
            var allAnimals = _dataBase.GetAnimalModels();
            for (var i = allCount - 1; i >= 0; --i)
            {
                var item = allAnimals[i];
                //волки жрут овцу
                if (item is SheepModel && !item.IsDead)
                {
                    WolfKiller_WolfKillSheep(animal, item);
                    break;
                }
                //волки жрут охотника
                else if (item is HunterModel && !item.IsDead)
                {
                    WolfKiller_WolfKillHunter(animal, item);
                    break;
                }
            }
            DataChangedInvoke(allAnimals);
        }

        public void HunterIsKiller(AnimalModel animal)
        {
            var allCount = _dataBase.AnimalModelCount<AnimalModel>();
            var allAnimals = _dataBase.GetAnimalModels();
            for (var i = allCount - 1; i >= 0; --i)
            {
                var item = allAnimals[i];
                //охотник валит волка
                if (item is WolfModel && !item.IsDead)
                {
                    HunterKiller_HunterKillWolf(animal, item);
                    break;
                }
            }
            DataChangedInvoke(allAnimals);
        }

        public void HunterKiller_HunterKillWolf(AnimalModel animal, AnimalModel item)
        {
            double hunterLiveCount = _dataBase.animalLiveCount<HunterModel>();
            if (hunterLiveCount <= 1)
            {
                StopTimer();
            }
            WhoKilledWho(item, animal);
            DuckFlyAway();
            string message = $"Охотник {animal.Name} завалил волка {item.Name}";
            NotifyKillInvoke(message, KillerAnnotation.HUNTER_KILL_WOLF);
            _dataBase.Update(animal);
            FillPrey(animal, item, AnimalType.HUNTER);
            //волк валит охотника
            HunterKiller_WolfKillHunter(animal, item);
        }

        public void HunterKiller_WolfKillHunter(AnimalModel animal, AnimalModel item)
        {
            double hunterLiveCount = _dataBase.animalLiveCount<HunterModel>();
            double wolfLiveCount = _dataBase.animalLiveCount<WolfModel>();
            if (wolfLiveCount / 2 > hunterLiveCount && hunterLiveCount != 0)
            {
                animal.IsDead = true;
                item.Killer = animal.Name;
                DuckFlyAway();
                FillPrey(item, animal, AnimalType.WOLF);
                _dataBase.Update(animal);
                _dataBase.Update(item);
                string message = $"Волк {item.Name} разодрал охотника {animal.Name}";
                NotifyKillInvoke(message, KillerAnnotation.WOLF_EAT_HUNTER);
                StopTimer();
            }
        }

        public void WolfKiller_WolfKillHunter(AnimalModel animal, AnimalModel item)
        {
            double hunterLiveCount = _dataBase.animalLiveCount<HunterModel>();
            if (hunterLiveCount <= 1)
            {
                StopTimer();
            }
            WhoKilledWho(item, animal);
            DuckFlyAway();
            string message = $"Волк {animal.Name} разодрал охотника {item.Name}";
            NotifyKillInvoke(message, KillerAnnotation.WOLF_EAT_HUNTER);
            FillPrey(animal, item, AnimalType.WOLF);
        }

        public void WolfKiller_WolfKillSheep(AnimalModel animal, AnimalModel item)
        {
            var allCount = _dataBase.AnimalModelCount<AnimalModel>();
            var allAnimals = _dataBase.GetAnimalModels();
            WhoKilledWho(item, animal);
            FillPrey(animal, item, AnimalType.WOLF);
            string message = $"Волк {animal.Name} сожрал овцу {item.Name}";
            NotifyKillInvoke(message, KillerAnnotation.WOLF_EAT_SHEEP);
            for (int k = allCount - 1; k >= 0; --k)
            {
                var hunt = allAnimals[k];
                if (hunt is HunterModel && !hunt.IsDead)
                {
                    WolfKiller_HunterKillWolf(animal, hunt);
                    break;
                }
            }
        }

        public void WolfKiller_HunterKillWolf(AnimalModel animal, AnimalModel hunt)
        {
            DuckFlyAway();
            Timer _timer = new Timer(5000);
            _aTimer.Start();
            _timer.Elapsed += (o, args) => { WhoKilledWho(animal, hunt); };
            _timer.AutoReset = true;
            _timer.Enabled = true;
            Console.WriteLine("завалили съевшего овцу");
            FillPrey(hunt, animal, AnimalType.HUNTER);
            string message = $"Охотник {hunt.Name} завалил волка {animal.Name}";
            NotifyKillInvoke(message, KillerAnnotation.HUNTER_KILL_WOLF);
            _dataBase.Update(animal);
            _dataBase.Update(hunt);
            _timer.Stop();
            _timer.Dispose();
        }

        public void NotifyKillInvoke(string message, KillerAnnotation killAnnotation)
        {
            var dataTransfer = new DataTransferEventArgs
            {
                Message = message,
                TypeKiller = killAnnotation
            };
            Notify?.Invoke(this, dataTransfer);
        }

        public void DataChangedInvoke(List<AnimalModel> allAnimals)
        {
            var transferModels = new TransferModelsEventArgs
            {
                Model = allAnimals
            };
            DataChanged?.Invoke(this, transferModels);
        }

        public void GetListAnimals()
        {
            _dataBase.SelectTable().OrderBy(a => a.Order).ToList();
            _dataBase.SelectTableID();
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
                    return _dataBase.GetItem<SheepModel>(animalID);
                case AnimalType.DUCK:
                    return _dataBase.GetItem<DuckModel>(animalID);
                case AnimalType.WOLF:
                    return _dataBase.GetItem<WolfModel>(animalID);
                case AnimalType.HUNTER:
                    return _dataBase.GetItem<HunterModel>(animalID);
                default:
                    break;
            }
            return null;
        }

        public string TextKill(AnimalModel animal)
        {
            if (animal.Killer != null)
            {
                if (animal is WolfModel)
                {
                    return $"This {AnimalType.WOLF} tear to pieces {animal.Killer}";
                }
                if (animal is HunterModel)
                {
                    return $"This {AnimalType.HUNTER} just kill a {animal.Killer}";
                }
            }
            return TypeOfAnimal(animal).ToString();
        }

        public string NameofKiller(AnimalModel animal)
        {
            if (animal.IsDead)
            {
                var killName = _dataBase.GetKillerID<Prey>(animal);
                if (animal is SheepModel)
                {
                    return $"This {AnimalType.SHEEP} eliminated by {killName}";
                }
                if (animal is WolfModel)
                {
                    return $"This {AnimalType.WOLF} is killed by a hunter {killName}";
                }
                if (animal is HunterModel)
                {
                    return $"This {AnimalType.HUNTER} is tear to pieces by a wolf {killName}";
                }
            }
            return "";
        }

        public void WhoKilledWho(AnimalModel sacrifice, AnimalModel killer)
        {
            Console.WriteLine("отработка WhoKilledWho");
            sacrifice.IsDead = true;
            killer.Killer = sacrifice.Name;
            _dataBase.Update(sacrifice);
            _dataBase.Update(killer);
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
            _dataBase.Delete<DuckModel>();
            duckCount = 0;
        }

        public void FillPrey(AnimalModel killer, AnimalModel victim, AnimalType typeOfKiller)
        {
            _prey.KillerId = killer.Id;
            _prey.VictimId = victim.Id;
            _prey.TypeOfKiller = (int)typeOfKiller;
            _dataBase.InsertID(_prey);
        }

        public void StartTimer(AnimalModel animal)
        {
            double wolfLiveCount = _dataBase.animalLiveCount<WolfModel>();
            if (_aTimer.Enabled == false)
            {
                _aTimer.Interval = 5000;
                _aTimer.Elapsed += (o, args) => {AnimalKiller(animal); };
                _aTimer.Start();
                _aTimer.AutoReset = true;
                _aTimer.Enabled = true;
                if (wolfLiveCount == 0)
                {
                    StopTimer();
                }
            }
        }

        public void StopTimer()
        {
            _aTimer.AutoReset = false;
            _aTimer.Enabled = false;
        }
    }

    public class DataTransferEventArgs : EventArgs
    {
        public string Message { get; set; }
        public KillerAnnotation TypeKiller { get; set; }
    }

    public class TransferModelsEventArgs : EventArgs
    {
        public List<AnimalModel> Model { get; set; }
    }
}
