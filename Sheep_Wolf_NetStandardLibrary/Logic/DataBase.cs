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
        void Insert(AnimalModel animal);
        void Update(AnimalModel animal);
        void Delete<T>() where T : AnimalModel, new();
        AnimalModel GetItem<T>(int N) where T: AnimalModel, new();
    }

    public class DataBase : IDataBase
    {

        private readonly string dbPath = Path.Combine(Environment.GetFolderPath
                                         (Environment.SpecialFolder.Personal), "dataBase.db3");
        public IEnumerable<AnimalModel> SelectTable()
        {
            var connection = new SQLiteConnection(dbPath);
            connection.CreateTable<SheepModel>();
            connection.CreateTable<WolfModel>();
            connection.CreateTable<DuckModel>();
            var tableSheep = connection.Table<SheepModel>();
            var tableWolf = connection.Table<WolfModel>();
            var tableDuck = connection.Table<DuckModel>();
            var animalArray = tableSheep.Union(tableWolf.Union<AnimalModel>(tableDuck));
            return animalArray;
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
    }
}