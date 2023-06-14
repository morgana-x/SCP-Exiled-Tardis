using Exiled;
using Exiled.API.Features;
using Hints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Exiled.API.Enums;
namespace TardisPlugin
{

    public class EventHandlers
    {
        Vector3 TardisLocation;
        static System.Random rnd = new System.Random();
        Tardis _Tardis = new TardisPlugin.Tardis();

        List<Room> RoomList = new List<Room>();

        List<RoomType> StartRoomPool = new List<RoomType>()
        {
            RoomType.LczGlassBox,
            RoomType.Lcz914,
            RoomType.HczServers,
            RoomType.HczNuke,
            RoomType.Hcz106
        };
        public void onMapGenerated()
        {
            //Log.Info("Spawning tardis!");
            RoomType TardisRoomType = StartRoomPool.RandomItem();
            Room TardisRoom = Room.Get(TardisRoomType);
            _Tardis.spawnTardisModel(TardisRoom.Position, new Vector3(24, 991, -40));
            _Tardis.InitialiseTardis(_Tardis);
            TardisLocation = TardisRoom.Position;
            Log.Info("Tardis spawned at " + TardisRoom.RoomName + "(" + TardisRoom.Position.ToString() + ")");
        }

        public void OnPlayerSpawned(Exiled.Events.EventArgs.Player.SpawnedEventArgs ev)
        {
            ev.Player.Teleport(_Tardis.policeBox_Door.Position + new Vector3(0, 0, 1.52f) + (Vector3.up * 0.2f));
        }

        public void OnPlayerPressInteract(Exiled.Events.EventArgs.Player.SearchingPickupEventArgs ev)
        {
           // Log.Info("Player pressed E");
            //ev.Player.ShowHint("Pressed E");

            _Tardis.ProcessButtons(_Tardis, ev.Player, ev.Pickup);
            ev.IsAllowed = false;

            /*RaycastHit hit;
            Transform transform = ev.Player.CameraTransform;
            int layerMask = 1 << 8;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20f, layerMask))
            {
                Log.Info("Player ray cast hit something");
                _Tardis.ProcessButtons(_Tardis, ev.Player, hit.transform);
            }*/
        }

    }
}