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

        public async Task SendMessage(long id, int x, int y, string groupId)
        {
            updatePlayerPosition(id, new Position(x,y));
            await Clients.Group(groupId).SendAsync("PlayerPositions", Players.Select(i => i.Value).ToList());
        }

        public async Task Join(string name, float x, float y, string groupId)
        {
            var id = Id++;
            Players.Add(id, new Player(id, name, new Position(x, y)));
            await Clients.Caller.SendAsync("ConfirmedID", id);
            await Clients.Group(groupId).SendAsync("PlayerJoined", Players.Select(i => i.Value).ToList());
        }

        private void updatePlayerPosition(long id, Position position) {
            Players[id] = new Player(id, Players[id].Name, position);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }


}
