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
        AnimalModel GetItem<T>(int N) where T: AnimalModel, new();
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
            var tablePrey = connection.Table<Prey>();
            return tablePrey;
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

        public AnimalModel GetItem<T>(int N) where T: AnimalModel, new()
        {
            var connection = new SQLiteConnection(dbPath);
            return connection.Table<T>().FirstOrDefault(a => a.Id == N);
        }

        public void InsertID(Prey prey)
        {
            var connection = new SQLiteConnection(dbPath);
            connection.Insert(prey);
        }
    }
}