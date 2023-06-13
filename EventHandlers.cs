using Exiled;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace TardisPlugin
{
 
    public class EventHandlers
    {
        Vector3 TardisLocation;
        static System.Random rnd = new System.Random();
        public void onMapGenerated()
        {
            Log.Info("Spawning tardis!");
            List<Room> RoomList =Exiled.API.Features.Room.List.ToList();
            int r = rnd.Next(RoomList.Count);
            Room TardisRoom = RoomList[r]; 
            Tardis _Tardis = new TardisPlugin.Tardis();
            _Tardis.spawnTardisModel(TardisRoom.Position, new Vector3(24,991,-40));
            _Tardis.InitialiseTardis(_Tardis);
            TardisLocation = TardisRoom.Position;
            Log.Info("Tardis spawned at " + TardisRoom.RoomName + "(" + TardisRoom.Position.ToString() + ")");
        }

        public void OnPlayerSpawned(Exiled.Events.EventArgs.Player.SpawnedEventArgs ev)
        {
            ev.Player.Teleport(TardisLocation + (Vector3.up * 10) );
        }
    }
}