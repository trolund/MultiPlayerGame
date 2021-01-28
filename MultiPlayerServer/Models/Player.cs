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
    }
}
