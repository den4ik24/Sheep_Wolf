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
        AnimalModel Transfer(int N, int typeOfAnimal);
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

        public AnimalModel Transfer(int N, int typeOfAnimal)
        {
            var connection = new SQLiteConnection(dbPath);

            
            if (typeOfAnimal == 0)
            {
                return connection.Table<SheepModel>().FirstOrDefault(a => a.Id == N);
            }
            else if (typeOfAnimal == 1)
            {
                return connection.Table<DuckModel>().FirstOrDefault(a => a.Id == N);
            }
            else
            {
                return connection.Table<WolfModel>().FirstOrDefault(a => a.Id == N);
            }
        }
    }
}
