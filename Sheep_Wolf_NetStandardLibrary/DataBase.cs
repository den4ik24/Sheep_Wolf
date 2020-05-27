using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    public class DataBase
    {
        private readonly string dbPath = Path.Combine(Environment.GetFolderPath
                                         (Environment.SpecialFolder.Personal), "dataBase.db3");

        public IEnumerable<AnimalModel> SelectTable()
        {
            var connection = new SQLiteConnection(dbPath);
            connection.CreateTable<SheepModel>();
            connection.CreateTable<WolfModel>();
            var tableSheep = connection.Table<SheepModel>();
            var tableWolf = connection.Table<WolfModel>();
            var animalArray = tableSheep.Union<AnimalModel>(tableWolf);
            return animalArray;
        }

        public void Insert(AnimalModel animal)
        {
            var connection = new SQLiteConnection(dbPath);
            connection.Insert(animal);
        }
    }
}
