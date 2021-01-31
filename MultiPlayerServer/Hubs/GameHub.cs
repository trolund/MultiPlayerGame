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

        public async Task SendMessage(long id, int x, int y)
        {
            updatePlayerPosition(id, new Position(x,y));
            await Clients.Others.SendAsync("PlayerPositions", Players.ToArray());
        }

        public async Task Join(string name, float x, float y)
        {
            var id = Id++;
            Players.Add(id, new Player(id, name, new Position(x, y)));
            await Clients.Caller.SendAsync("ConfirmedID", id);
            await Clients.Others.SendAsync("PlayerJoined", Players.ToArray());
        }

        private void updatePlayerPosition(long id, Position position) {
            Players[id] = new Player(id, Players[id].Name, position);
        }
    }


}
