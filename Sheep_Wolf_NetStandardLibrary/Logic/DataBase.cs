using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    public interface IDataBase
    {
        IEnumerable<AnimalModel> SelectTable();
        IEnumerable<Prey> SelectTableID();
        void InsertID(Prey prey);
        void Insert(AnimalModel animal);
        void Update(AnimalModel animal);
        void Delete<T>() where T : AnimalModel, new();
        AnimalModel GetItem<T>(string N) where T: AnimalModel, new();
        bool nameVerification<T>(string N) where T : AnimalModel, new();
        int GetID<T>(string id) where T : Prey, new();
        string GetKillerID<T>(AnimalModel animal) where T : Prey, new();
        int countAnimal<T>() where T : AnimalModel, new();
        int animalLiveCount<T>() where T : AnimalModel, new();
        int AnimalModelCount<T>() where T : AnimalModel, new();
        List<AnimalModel> GetAnimalModels();
    }

    public class DataBase : IDataBase
    {
        private readonly string dbPath = Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dataBase.db3");

        public IEnumerable<AnimalModel> SelectTable()
        {
            var connection = new SQLiteConnection(dbPath);
            connection.CreateTable<SheepModel>();
            connection.CreateTable<WolfModel>();
            connection.CreateTable<DuckModel>();
            connection.CreateTable<HunterModel>();
            var tableSheep = connection.Table<SheepModel>();
            var tableWolf = connection.Table<WolfModel>();
            var tableDuck = connection.Table<DuckModel>();
            var tableHunter = connection.Table<HunterModel>();
            var animalArray = tableSheep.Union(tableWolf.Union(tableDuck.Union<AnimalModel>(tableHunter)));
            return animalArray;
        }

        public IEnumerable<Prey> SelectTableID()
        {
            var connection = new SQLiteConnection(dbPath);
            connection.CreateTable<Prey>();
            return connection.Table<Prey>();
        }

        public void Insert(AnimalModel animal)
        {
            var connection = new SQLiteConnection(dbPath);
            connection.Insert(animal);
        }

        public void Update(AnimalModel animal)
        {
            var connection = new SQLiteConnection(dbPath);
            connection.Update(animal);
        }

        public void Delete<T>() where T : AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            connection.DeleteAll<T>();
        }

        public AnimalModel GetItem<T>(string animalID) where T : AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            return connection.Table<T>().FirstOrDefault(a => a.Id == animalID);
        }

        public bool nameVerification<T>(string animalName) where T : AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            return connection.Table<T>().Where(a => a.Name == animalName).Any();
        }

        public void InsertID(Prey prey)
        {
            var connection = new SQLiteConnection(dbPath);
            connection.Insert(prey);
        }

        public string GetKillerID<T>(AnimalModel animal) where T : Prey, new()
        {
            var connection = new SQLiteConnection(dbPath);
            var victimModel = connection.Table<T>().FirstOrDefault(a => a.VictimId == animal.Id);
            var killID = victimModel.KillerId;
            var typeKiller = victimModel.TypeOfKiller;

            switch (typeKiller)
            {
                case (int)AnimalType.HUNTER:
                    return connection.Table<HunterModel>().FirstOrDefault(a => a.Id == killID).Name;
                case (int)AnimalType.WOLF:
                    return connection.Table<WolfModel>().FirstOrDefault(a => a.Id == killID).Name;
            }

            return null;
        }

        public int GetID<T>(string id) where T : Prey, new()
        {
            var connection = new SQLiteConnection(dbPath);
            return connection.Table<T>().Where(a => a.KillerId == id).Count();
        }

        public int countAnimal<T>() where T : AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            if (connection.Table<T>().Count() == 0)
            {
                return 0;
            }
            else
            {
                return connection.Table<T>().Max(a => a.Order);
            }
        }

        public int animalLiveCount<T>() where T : AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            return connection.Table<T>().Where(a => !a.IsDead).Count();
        }

        public int AnimalModelCount<T>() where T : AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            var tableSheep = connection.Table<SheepModel>().Count();
            var tableWolf = connection.Table<WolfModel>().Count();
            var tableDuck = connection.Table<DuckModel>().Count();
            var tableHunter = connection.Table<HunterModel>().Count();
            return tableSheep + tableWolf + tableDuck + tableHunter;
        }

        public List<AnimalModel> GetAnimalModels()
        {
            var connection = new SQLiteConnection(dbPath);
            var tableSheep = connection.Table<SheepModel>();
            var tableWolf = connection.Table<WolfModel>();
            var tableDuck = connection.Table<DuckModel>();
            var tableHunter = connection.Table<HunterModel>();
            return tableSheep.Union(tableWolf.Union(tableDuck.Union<AnimalModel>(tableHunter))).OrderBy(a => a.Order).ToList();
        }
    }
}