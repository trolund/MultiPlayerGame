using System;
namespace MultiPlayerServer.Models
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }

        public Player()
        {
        }

        public Player(long id, string name, Position position)
        {
            Id = id;
            Name = name;
            Position = position;
        }
    }
}
