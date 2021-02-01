using System;
namespace MultiPlayerServer.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }

        public Player()
        {
        }

        public Player(string id, string name, Position position)
        {
            Id = id;
            Name = name;
            Position = position;
        }
    }
}
