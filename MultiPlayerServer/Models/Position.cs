using System;
namespace MultiPlayerServer.Models
{
    public class Position
    {

        public float X { get; }
        public float Y { get; }

        public Position()
        {
        }

        public Position(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return "x: " + X + " y: " + Y;
        }
    }
}
