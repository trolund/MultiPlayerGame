using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MultiPlayerServer.Models;

namespace MultiPlayerServer.Hubs
{
    public class GameHub : Hub
    {
        public static Dictionary<long, Player> Players = new Dictionary<long, Player>();
        public static long Id = 0;

        public async Task SendMessage(long id, Position pos)
        {
            updatePlayerPosition(id, pos);
            await Clients.All.SendAsync("playerPositions", Players);
        }

        public async Task Join(string name, float x, float y)
        {

            Players.Add(Id++, new Player());
            await Clients.All.SendAsync("playerJoined", Players);
        }

        private void updatePlayerPosition(long id, Position position) {
            Player a;
            if (Players.TryGetValue(id, out a))
            {
                a.Position = position;
                Players.Remove(id);
                Players.Add(id, a);
            }

        }
    }


}
