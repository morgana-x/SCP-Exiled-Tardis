using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Enums;
using Exiled;
using Unity;
using MEC;
using Exiled.API.Features.Roles;

using AdminToys;
using Exiled.API.Features.Toys;
using System.Runtime.InteropServices;
using Exiled.API.Features.Pickups;

namespace TardisPlugin
{
    public class ButtonCallback
    {
        public void main(Player pl, Tardis tardis)
        {

        }
    }
    public class Tardis
    {
        static System.Random rnd = new System.Random();
        Primitive policeBox_Cube;
        public Primitive policeBox_Door;

        public Primitive shipInterior_Door;

        Vector3 TardisInteriorPosition;
        Vector3 TardisInteriorSpawnPosition;

        public Vector3 TardisExteriorDoorPosition;
        Primitive policeBox_Cube2;
        Primitive policeBox_Cube23;
        Primitive policeBox_Cube3;
        Primitive policeBox_Cube4;
        Primitive policeBox_CubeLight;
        Primitive policeBox_CubeTop;
        LightSourceToy policeBox_Light;

        Primitive policeBox_Window_Front_1;
        Primitive policeBox_Window_Front_2;
        Primitive policeBox_Window_Back_1;
        Primitive policeBox_Window_Back_2;
        Primitive policeBox_Window_Left_1;
        Primitive policeBox_Window_Left_2;
        Primitive policeBox_Window_Right_1;
        Primitive policeBox_Window_Right_2;

        Room SelectedRoomDestination = null;
        public Room CurrentRoom;
        bool IsSelectedRoomDestination = false;
        double lastTardisTeleport = 0;
        public IDictionary<Pickup, string> ButtonLocations = new Dictionary<Pickup, string>();
        public void AddButton(Vector3 Location, ItemType itemModel, string id)
        {

            /* Primitive ButtonModel = Primitive.Create(PrimitiveType.Cube);
             ButtonModel.Color = color;
             ButtonModel.Position = Location;
             ButtonModel.Scale = new Vector3(0.1f, 0.1f, 0.1f);
             ButtonModel.Collidable = true;
             ButtonModel.Spawn();*/
            Pickup ButtonInteract = Pickup.Create(itemModel);//(ItemType.Medkit);
            ButtonInteract.Position = Location;
            ButtonInteract.Transform.position = Location;
            // ButtonInteract.IsLocked = true;
            ButtonInteract.GameObject.GetComponent<Rigidbody>().isKinematic = true;
            ButtonInteract.PickupTime = 1000f;
            ButtonInteract.Spawn();
            ButtonLocations.Add(ButtonInteract, id);
        }
        public void spawnTardisModel(Vector3 ExteriorPosition, Vector3 InteriorPosition)
        {

            Color TardisExteriorColor = Color.blue;

            // Exterior of tardis

            //policeBox_Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //policeBox_Cube.transform.position = ExteriorPosition;
            //policeBox_Cube.transform.localScale = new Vector3(2f, 6f, 2f);
            //policeBox_Cube.GetComponent<Renderer>().material.color = TardisExteriorColor;

            policeBox_Cube = Primitive.Create(PrimitiveType.Cube);
            policeBox_Cube.Position = ExteriorPosition + (Vector3.up * 1.125f);
            policeBox_Cube.Color = TardisExteriorColor;
            policeBox_Cube.Scale = new Vector3(1.5f, 2.25f, 1.5f);
            policeBox_Cube.Collidable = true;
            policeBox_Cube.Spawn();

            float startHeight = 2.25f;
            policeBox_Cube2 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Cube2.Position = ExteriorPosition  + (Vector3.up * (startHeight));
            policeBox_Cube2.Color = TardisExteriorColor;
            policeBox_Cube2.Scale = new Vector3(1.2f, 0.2f, 1.2f);
            policeBox_Cube2.Collidable = true;
            policeBox_Cube2.Spawn();

            policeBox_Cube23 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Cube23.Position = ExteriorPosition + (Vector3.up * (startHeight));
            policeBox_Cube23.Color = TardisExteriorColor;
            policeBox_Cube23.Scale = new Vector3(1f, 0.4f, 1f);
            policeBox_Cube23.Collidable = true;
            policeBox_Cube2.Spawn();

            policeBox_Cube3 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Cube3.Position = ExteriorPosition  + (Vector3.up * (startHeight + 0.2f));
            policeBox_Cube3.Color = TardisExteriorColor;
            policeBox_Cube3.Scale = new Vector3(0.5f, 0.1f, 0.5f);
            policeBox_Cube3.Collidable = false;
            policeBox_Cube3.Spawn();

            policeBox_Cube4 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Cube4.Position = ExteriorPosition  + (Vector3.up * (startHeight + 0.2f + 0.1f));
            policeBox_Cube4.Color = TardisExteriorColor;
            policeBox_Cube4.Scale = new Vector3(0.4f, 0.25f, 0.4f);
            policeBox_Cube4.Collidable = false;
            policeBox_Cube4.Spawn();

            policeBox_CubeLight = Primitive.Create(PrimitiveType.Cube);
            policeBox_CubeLight.Position = ExteriorPosition  + (Vector3.up * (startHeight + 0.2f + 0.1f + 0.2f));
            policeBox_CubeLight.Color = Color.white;
            policeBox_CubeLight.Scale = new Vector3(0.25f, 0.1f, 0.25f);
            policeBox_CubeLight.Collidable = false;
            policeBox_CubeLight.Spawn();

            policeBox_CubeTop = Primitive.Create(PrimitiveType.Cube);
            policeBox_CubeTop.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f + 0.1f + 0.2f + 0.1f));
            policeBox_CubeTop.Color = Color.blue;
            policeBox_CubeTop.Scale = new Vector3(0.28f, 0.15f, 0.28f);
            policeBox_CubeTop.Collidable = false;
            policeBox_CubeTop.Spawn();

            policeBox_Light = new LightSourceToy();
            policeBox_Light.LightColor = Color.white;
            policeBox_Light.LightShadows = true;
            policeBox_Light.LightIntensity = 1;
            policeBox_Light.LightRange = 2;
            policeBox_Light.Position = ExteriorPosition + (Vector3.up * 5f);
            //  policeBox_Light.enabled = true;

            float forwardOffset = 0.75f;

            policeBox_Door = Primitive.Create(PrimitiveType.Cube);
            policeBox_Door.Position = ExteriorPosition + (Vector3.up * 1) + new Vector3(0, 0, forwardOffset);
            policeBox_Door.Scale = new Vector3(1f, 2f, 0.1f);
            policeBox_Door.Color = Color.black;
            policeBox_Door.Collidable = false;
            policeBox_Door.Spawn();

            float windowHeight = 0.5f;
            float windowWidth = 0.5f;
            float windowDepth = 0.05f;

            float windowScaleFactor = 1.3f;
            policeBox_Window_Front_1 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Front_1.Position = ExteriorPosition + (new Vector3(-windowWidth / 2, 0, forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Front_1.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Front_1.Color = Color.white;
            policeBox_Window_Front_1.Collidable = false;
            policeBox_Window_Front_1.Spawn();
            policeBox_Window_Front_2 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Front_2.Position = ExteriorPosition + (new Vector3(windowWidth / 2, 0, forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Front_2.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Front_2.Color = Color.white;
            policeBox_Window_Front_2.Collidable = false;
            policeBox_Window_Front_2.Spawn();

            policeBox_Window_Back_1 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Back_1.Position = ExteriorPosition + (new Vector3(-windowWidth / 2, 0, -forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Back_1.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Back_1.Color = Color.white;
            policeBox_Window_Back_1.Collidable = false;
            policeBox_Window_Back_1.Spawn();
            policeBox_Window_Back_2 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Back_2.Position = ExteriorPosition + (new Vector3(windowWidth / 2, 0, -forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Back_2.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Back_2.Color = Color.white;
            policeBox_Window_Back_2.Collidable = false;
            policeBox_Window_Back_2.Spawn();


            policeBox_Window_Left_1 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Left_1.Position = ExteriorPosition + (new Vector3(-0.8f, 0, -windowWidth / 2)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Left_1.Scale = new Vector3(windowDepth, windowHeight, windowWidth / windowScaleFactor);
            policeBox_Window_Left_1.Color = Color.white;
            policeBox_Window_Left_1.Collidable = false;
            policeBox_Window_Left_1.Spawn();
            policeBox_Window_Left_2 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Left_2.Position = ExteriorPosition + (new Vector3(-0.8f, 0, windowWidth / 2)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Left_2.Scale = new Vector3(windowDepth, windowHeight, windowWidth / windowScaleFactor);
            policeBox_Window_Left_2.Color = Color.white;
            policeBox_Window_Left_2.Collidable = false;
            policeBox_Window_Left_2.Spawn();

            policeBox_Window_Right_1 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Right_1.Position = ExteriorPosition + (new Vector3(0.8f, 0, -windowWidth / 2)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Right_1.Scale = new Vector3(windowDepth, windowHeight, windowWidth / windowScaleFactor);
            policeBox_Window_Right_1.Color = Color.white;
            policeBox_Window_Right_1.Collidable = false;
            policeBox_Window_Right_1.Spawn();
            policeBox_Window_Right_2 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Right_2.Position = ExteriorPosition + (new Vector3(0.8f, 0, windowWidth / 2)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Right_2.Scale = new Vector3(windowDepth, windowHeight, windowWidth / windowScaleFactor);
            policeBox_Window_Right_2.Color = Color.white;
            policeBox_Window_Right_2.Collidable = false;
            policeBox_Window_Right_2.Spawn();



            TardisInteriorPosition = new Vector3(39.24f, 1014.11f, -32.23f);
            TardisInteriorSpawnPosition = new Vector3(41.05f, 1014.11f, -32.27f);

            TardisExteriorDoorPosition = policeBox_Door.Position + new Vector3(0, 0, 1.52f) + (Vector3.up * 0.2f);
            shipInterior_Door = Primitive.Create(PrimitiveType.Cube);
            shipInterior_Door.Color = Color.black;
            shipInterior_Door.Scale = new Vector3(0.2f, 2f, 1f);
            shipInterior_Door.Position = new Vector3(42.50f, 1014.11f +1f, -32.168f);
            shipInterior_Door.Collidable = false;
            shipInterior_Door.Spawn();

            Primitive ConsoleTube = Primitive.Create(PrimitiveType.Cylinder);
            ConsoleTube.Color = Color.cyan;
            ConsoleTube.Scale = new Vector3(0.6f, 3f, 0.6f);
            ConsoleTube.Position = TardisInteriorPosition;
            ConsoleTube.Collidable = true;
            ConsoleTube.Spawn();

            Primitive ConsoleBase = Primitive.Create(PrimitiveType.Cube);
            ConsoleBase.Color = Color.grey;
            ConsoleBase.Position = TardisInteriorPosition + (Vector3.down * 0.5f);
            ConsoleBase.Scale = new Vector3(0.8f, 1f, 0.8f);
            ConsoleBase.Collidable = true;
            ConsoleBase.Spawn();

            Primitive ConsoleBase2 = Primitive.Create(PrimitiveType.Cube);
            ConsoleBase2.Color = Color.gray;
            ConsoleBase2.Position = TardisInteriorPosition + (Vector3.up * 0.5f) + (Vector3.down * 0.5f);
            ConsoleBase2.Scale = new Vector3(1.3f, 0.1f, 1.3f);
            ConsoleBase2.Collidable = true;
            ConsoleBase2.Spawn();


            Vector3 ConsoleSurfaceLevel_MedKit = TardisInteriorPosition + (Vector3.up * 0.51f) + (Vector3.down * 0.5f);
            Vector3 ConsoleSurfaceLevel_Coin = TardisInteriorPosition + (Vector3.up * 0.55f) + (Vector3.down * 0.5f);

            // FRONT, NAVIGATION STUFF
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, 0f), ItemType.SCP2176, "teleport_gateB");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, 0.1f), ItemType.SCP2176, "teleport_gateA");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, 0.2f), ItemType.SCP2176, "random_move_surface");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, 0.3f), ItemType.SCP2176, "teleport_DClassSpawn");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, 0.4f), ItemType.SCP2176, "teleport_nuke");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, 0.5f), ItemType.SCP2176, "teleport_Larry");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, 0.6f), ItemType.SCP2176, "teleport_elevatorHCZA");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, -0.6f), ItemType.SCP2176, "teleport_elevatorHCZB");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, -0.5f), ItemType.SCP2176, "teleport_glassbox");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, -0.4f), ItemType.SCP2176, "teleport_micro");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, -0.3f), ItemType.SCP2176, "teleport_914");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, -0.2f), ItemType.SCP2176, "teleport_Peanut");
            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.3f, 0f, -0.1f), ItemType.SCP2176, "teleport_pocket");

            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.5f, 0f, 0f), ItemType.SCP018, "activate_teleport");


            // RIGHT, SCANNING / OTHER UTILITIES

            AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0f, 0f, 0.5f), ItemType.Medkit, "scan_facility_scp");
            //  AddButton(ConsoleSurfaceLevel_Coin + new Vector3(0.5f, 0f, -0.1f), ItemType.SCP2176, "teleport_49");
        }
        public void moveTardisModel(Vector3 ExteriorPosition)
        {

            Color TardisExteriorColor = Color.blue;

            policeBox_Cube.Position = ExteriorPosition + (Vector3.up * 1.125f);
            policeBox_Cube.Color = TardisExteriorColor;
            policeBox_Cube.Spawn();

            float startHeight = 2.25f;
            policeBox_Cube2.Position = ExteriorPosition + (Vector3.up * (startHeight));
            policeBox_Cube2.Color = TardisExteriorColor;
            policeBox_Cube2.Spawn();

            policeBox_Cube23.Position = ExteriorPosition + (Vector3.up * (startHeight));
            policeBox_Cube23.Color = TardisExteriorColor;
            policeBox_Cube2.Spawn();

            policeBox_Cube3.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f));
            policeBox_Cube3.Color = TardisExteriorColor;
            policeBox_Cube3.Spawn();

            policeBox_Cube4.Position = ExteriorPosition  + (Vector3.up * (startHeight + 0.2f + 0.1f));
            policeBox_Cube4.Color = TardisExteriorColor;
            policeBox_Cube4.Spawn();

            policeBox_CubeLight.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f + 0.1f + 0.2f));
            policeBox_CubeLight.Color = Color.white;
            policeBox_CubeLight.Spawn();

            policeBox_CubeTop.Position = ExteriorPosition  + (Vector3.up * (startHeight + 0.2f + 0.1f + 0.2f + 0.1f));
            policeBox_CubeTop.Color = Color.blue;
            policeBox_CubeTop.Spawn();

            policeBox_Light.LightColor = Color.white;
            policeBox_Light.LightShadows = true;
            policeBox_Light.LightIntensity = 1;
            policeBox_Light.LightRange = 2;
            policeBox_Light.Position = ExteriorPosition  + (Vector3.up * 5f);
       
            //  policeBox_Light.enabled = true;

            float forwardOffset = 0.75f;

            policeBox_Door.Position = ExteriorPosition + (Vector3.up * 1.125f) + new Vector3(0, 0, forwardOffset);
            policeBox_Door.Color = Color.black;
            policeBox_Door.Spawn();

            float windowHeight = 0.5f;
            float windowWidth = 0.5f;
            float windowDepth = 0.05f;

            float windowScaleFactor = 1.5f;

            Vector3 ExteriorPositionWindow = ExteriorPosition + (Vector3.up * 0.25f);

            policeBox_Window_Front_1.Position = ExteriorPositionWindow + (new Vector3(-windowWidth / 2, 0, forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Front_1.Color = Color.white;
            policeBox_Window_Front_1.Spawn();

            policeBox_Window_Front_2.Position = ExteriorPositionWindow + (new Vector3(windowWidth / 2, 0, forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Front_2.Color = Color.white;
            policeBox_Window_Front_2.Spawn();

            policeBox_Window_Back_1.Position = ExteriorPositionWindow + (new Vector3(-windowWidth / 2, 0, -forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Back_1.Color = Color.white;
            policeBox_Window_Back_1.Spawn();

            policeBox_Window_Back_2.Position = ExteriorPositionWindow + (new Vector3(windowWidth / 2, 0, -forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Back_2.Color = Color.white;
            policeBox_Window_Back_2.Spawn();


            policeBox_Window_Left_1.Position = ExteriorPositionWindow + (new Vector3(-0.8f, 0, -windowWidth / 2)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Left_1.Color = Color.white;
            policeBox_Window_Left_1.Spawn();    

            policeBox_Window_Left_2.Position = ExteriorPositionWindow + (new Vector3(-0.8f, 0, windowWidth / 2)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Left_2.Color = Color.white;
            policeBox_Window_Left_2.Spawn();

            policeBox_Window_Right_1.Position = ExteriorPositionWindow + (new Vector3(0.8f, 0, -windowWidth / 2)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Right_1.Color = Color.white;
            policeBox_Window_Right_1.Spawn();

            policeBox_Window_Right_2.Position = ExteriorPositionWindow + (new Vector3(0.8f, 0, windowWidth / 2)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Right_2.Color = Color.white;
            policeBox_Window_Right_2.Spawn();

        }
        public void rotateTardisDoor(Room room)
        {
            Vector3 fwd = room.transform.forward;
            policeBox_Door.Position = policeBox_Cube.Position + (fwd *0.75f);
            policeBox_Door.Rotation = room.transform.localRotation;
        }

            public IEnumerator<float> CheckEnterTardis(float radius, Primitive ExtDoor, Vector3 IntDoorPos)
        {
            while (true)
            {
                Vector3 ExtDoorPos = ExtDoor.Position;
                foreach (Player pl in Player.List)
                {
                   // Log.Info("Checking " + pl.Nickname);
                    if (Vector3.Distance(pl.Position, ExtDoorPos) < radius)
                    {
                       // Log.Info("Sent inside tardis");
                        pl.Teleport(TardisInteriorSpawnPosition);
                        continue;
                    }
                    if (Vector3.Distance(pl.Position, IntDoorPos) < radius)
                    {
                       // Log.Info("Sent outside tardis");
                        pl.Teleport(ExtDoorPos + new Vector3(0,0, 1.52f) + (Vector3.up * 0.2f));
                        continue;
                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }
        public void InitialiseTardis(Tardis tardis)
        {
            Timing.RunCoroutine(tardis.CheckEnterTardis(1.2f, policeBox_Door, shipInterior_Door.Position)); //shipInterior_Door.Position));
        }

        public void GotoRoom(Tardis tardis, Room TardisRoom)
        {
            Log.Info( "Tardis moving to " + TardisRoom.RoomName + "(" + TardisRoom.Position.ToString() + ")");
            tardis.moveTardisModel(TardisRoom.Position);
           // tardis.rotateTardisDoor(TardisRoom);
            CurrentRoom = TardisRoom;
        }

        public void SelectRoomViaPlayer(Tardis tardis, Room TardisRoom, Player player)
        {
          
            SelectedRoomDestination = TardisRoom;
            player.ShowHint("Selected " + TardisRoom.Name);
        }
        public void GotoRoomViaPlayer(Tardis tardis, Room TardisRoom, Player player)
        {
            if (CurrentRoom == TardisRoom)
            {
                player.ShowHint("You are already at this destination", 5f);
                return;
            }
            if ((TardisRoom.Zone == ZoneType.LightContainment) && Exiled.API.Features.Map.IsLczDecontaminated)
            {
                player.ShowHint("ABORTING, Volatile Gas Detected at " + TardisRoom.RoomName, 5f);
                return;
            }
            if ( (TardisRoom.Zone != ZoneType.Surface) && Exiled.API.Features.Warhead.IsDetonated)
            {
                player.ShowHint("ABORTING, Excess Radiation detected at " + TardisRoom.RoomName, 5f);
                return;
            }
            if (lastTardisTeleport > Round.ElapsedTime.TotalSeconds)
            {
                double cooldown = Math.Round(-Round.ElapsedTime.TotalSeconds + lastTardisTeleport,2);
                player.ShowHint("Wait " + cooldown.ToString() + "s before teleporting again", 5f );
                return;
            }

            foreach (Player pl in TardisRoom.Players)
            {
                if (Vector3.Distance(pl.Position, TardisRoom.Position) < 1.6f)
                {
                    player.ShowHint("Error! Obstruction at destination detected.");
                    return;
                }
            }
            Log.Info("Tardis moving to " + TardisRoom.RoomName + "(" + TardisRoom.Position.ToString() + ")");
            player.ShowHint("Moving to " + TardisRoom.Name);
            tardis.moveTardisModel(TardisRoom.Position);
            // tardis.rotateTardisDoor(TardisRoom);
            SelectedRoomDestination = null;
            CurrentRoom = TardisRoom;
            lastTardisTeleport = Round.ElapsedTime.TotalSeconds + 15;
        }

        public void ProcessButtonCommand(Tardis tardis, Player pl, string id)
        {
            if (id == "random_move")
            {
                List<Room> RoomList = Exiled.API.Features.Room.List.ToList();
                int r = rnd.Next(RoomList.Count);
                Room TardisRoom = RoomList[r];
                SelectRoomViaPlayer(tardis, TardisRoom,pl);
            }
            if (id == "random_move_heavy")
            {
                Room TardisRoom = Exiled.API.Features.Room.Random(ZoneType.HeavyContainment);
                SelectRoomViaPlayer(tardis, TardisRoom,pl);
            }
            if (id == "random_move_light")
            {
                Room TardisRoom = Exiled.API.Features.Room.Random(ZoneType.LightContainment);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "random_move_entrance")
            {
                Room TardisRoom = Exiled.API.Features.Room.Random(ZoneType.Entrance);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "random_move_surface")
            {
                Room TardisRoom = Exiled.API.Features.Room.Random(ZoneType.Surface);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_pocket")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.Pocket);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_914")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.Lcz914);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_micro")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.HczHid);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_nuke")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.HczNuke);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }

            if (id == "teleport_gateB")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.EzGateB);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_gateA")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.EzGateA);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_DClassSpawn")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.LczClassDSpawn);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_Peanut")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.HczServers);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_Larry")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.Hcz106);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_elevatorHCZA")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.HczElevatorA);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_elevatorHCZB")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.HczElevatorB);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "teleport_glassbox")
            {
                Room TardisRoom = Exiled.API.Features.Room.Get(RoomType.LczGlassBox);
                SelectRoomViaPlayer(tardis, TardisRoom, pl);
            }
            if (id == "scan_facility_scp")
            {
                int numSCPS = Exiled.API.Features.Round.SurvivingSCPs;
                pl.ShowHint( numSCPS.ToString() + " Anomolous Sentient Life Forms Detected", 5f);
            }
            if (id == "scan_facility_mtf")
            {
          
            }
            if (id == "activate_teleport")
            {
                if (SelectedRoomDestination==null)
                {
                    pl.ShowHint("No destination selected!", 5f);                    
                    return;
                }
                GotoRoomViaPlayer(tardis, SelectedRoomDestination, pl);
            }
        }
        public bool ProcessButtons(Tardis tardis, Player pl, Pickup pickup)
        {
            //Log.Info("Start check");
            foreach (KeyValuePair<Pickup, string> buttonObj in ButtonLocations)
            {
                if ( buttonObj.Key != pickup   )   
                    continue;
                ProcessButtonCommand(tardis, pl, buttonObj.Value);
                return true;
            }
            return false;
        }
   
    }
}
