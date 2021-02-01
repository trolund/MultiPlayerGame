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
        public static Dictionary<string, Player> games = new Dictionary<string, Player>();
        int Id = 0;

        public async Task SendMessage(string id, int x, int y, string groupId)
        {
            updatePlayerPosition(id, new Position(x,y));
            await Clients.Group(groupId).SendAsync("PlayerPositions", games.Select(i => i.Value).ToList());
        }

        public async Task Join(string name, float x, float y, string groupId)
        {
            var id = new Guid().ToString();
            games.Add(id, new Player(id, name, new Position(x, y)));
            await Clients.Caller.SendAsync("ConfirmedID", id);
            await Clients.Group(groupId).SendAsync("PlayerJoined", games.Select(i => i.Value).ToList());
        }

        private void updatePlayerPosition(string id, Position position) {
            games[id] = new Player(id, games[id].Name, position);
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
