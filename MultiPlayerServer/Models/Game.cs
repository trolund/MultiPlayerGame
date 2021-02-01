using System;
using System.Collections.Generic;
using MultiPlayerServer.Helpers;

namespace MultiPlayerServer.Models
{
    public class Game
    {
        public string Name { get; set; }
        public int GameId { get; set; }
        public Dictionary<Guid, Player> games { get; set; }

        public Game(string name, int id)
        {
            Name = name;
            GameId = id;
            games = new Dictionary<Guid, Player>();
        }

        public Player GetPlayer(Guid playerId)
        {
            return games[playerId];
        }

        public Guid AddPlayer(Player p)
        {
            Guid id = new Guid();
            games.Add(id, p);
            return id;
        }


    }
}
