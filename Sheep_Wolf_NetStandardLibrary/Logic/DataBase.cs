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
        int GetID<T>(string id) where T : Prey, new();
        string GetKillerID<T>(AnimalModel animal) where T : Prey, new();
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

        public void Delete<T>() where T: AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            connection.DeleteAll<T>();
        }

        public AnimalModel GetItem<T>(string animalID) where T: AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            return connection.Table<T>().FirstOrDefault(a => a.Id == animalID);
        }

        public void InsertID(Prey prey)
        {
            var connection = new SQLiteConnection(dbPath);
            connection.Insert(prey);
        }

        public string GetKillerID<T>(AnimalModel animal) where T : Prey, new()
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

            //if (typeKiller == (int)AnimalType.HUNTER)
            //{
            //    //return connection.Table<HunterModel>().FirstOrDefault(a => a.Name == killID).Name;

            //    return connection.Table<HunterModel>().AsEnumerable().FirstOrDefault(a =>
            //    {
            //        if (a.Id == killID)
            //        {
            //            return true;
            //        }
            //        return false;
            //    }).Name;
            //}

            //if (typeKiller == (int)AnimalType.WOLF)
            //{
            //    return connection.Table<WolfModel>().FirstOrDefault(a => a.Name == killID).Name;
            //}

            return null;
        }

        public int GetID<T>(string id) where T : Prey, new()
        {
            Console.WriteLine($"GetID: {id}");

            var connection = new SQLiteConnection(dbPath);
            var o = connection.Table<T>().Where(a => a.KillerId == id);
            ////////////////////////////////////////////////////////////////////
            var a1 = connection.Table<T>();
            foreach(var abc in a1)
            {
                Console.WriteLine($"KillerId: {abc.KillerId} VictimId: {abc.VictimId} ");
            }
            ////////////////////////////////////////////////////////////////////
            return o.Count();
        }
    }
}