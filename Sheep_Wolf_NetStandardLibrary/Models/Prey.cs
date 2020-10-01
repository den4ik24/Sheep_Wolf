﻿using SQLite;

namespace Sheep_Wolf_NetStandardLibrary
{
    [Table("Prey")]
    public class Prey
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int killerId { get; set; }
        public int victimId { get; set; }

        public Prey()
        {

        }
    }
}
