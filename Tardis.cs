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

namespace TardisPlugin
{
    public class Tardis
    {

        Primitive policeBox_Cube;
        Primitive policeBox_Door;

        Primitive shipInterior_Door;

        Vector3 TardisInteriorPosition;

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


        public void spawnTardisModel(Vector3 ExteriorPosition, Vector3 InteriorPosition)
        {

            Color TardisExteriorColor = Color.blue;

            // Exterior of tardis

            //policeBox_Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //policeBox_Cube.transform.position = ExteriorPosition;
            //policeBox_Cube.transform.localScale = new Vector3(2f, 6f, 2f);
            //policeBox_Cube.GetComponent<Renderer>().material.color = TardisExteriorColor;

            policeBox_Cube = Primitive.Create(PrimitiveType.Cube);
            policeBox_Cube.Position = ExteriorPosition;
            policeBox_Cube.Color = TardisExteriorColor;
            policeBox_Cube.Scale = new Vector3(1.5f, 4.5f, 1.5f);
            policeBox_Cube.Collidable = true;
            policeBox_Cube.Spawn();

            float startHeight = 2.25f;
            policeBox_Cube2 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Cube2.Position = ExteriorPosition + (Vector3.up * (startHeight));
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
            policeBox_Cube3.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f));
            policeBox_Cube3.Color = TardisExteriorColor;
            policeBox_Cube3.Scale = new Vector3(0.5f, 0.1f, 0.5f);
            policeBox_Cube3.Collidable = false;
            policeBox_Cube3.Spawn();

            policeBox_Cube4 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Cube4.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f + 0.1f));
            policeBox_Cube4.Color = TardisExteriorColor;
            policeBox_Cube4.Scale = new Vector3(0.4f, 0.25f, 0.4f);
            policeBox_Cube4.Collidable = false;
            policeBox_Cube4.Spawn();

            policeBox_CubeLight = Primitive.Create(PrimitiveType.Cube);
            policeBox_CubeLight.Position = ExteriorPosition + (Vector3.up*(startHeight + 0.2f + 0.1f + 0.25f));
            policeBox_CubeLight.Color = Color.white;
            policeBox_CubeLight.Scale = new Vector3(0.25f, 0.25f, 0.25f);
            policeBox_CubeLight.Collidable = false;
            policeBox_CubeLight.Spawn();

            policeBox_CubeTop = Primitive.Create(PrimitiveType.Cube);
            policeBox_CubeTop.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f + 0.1f + 0.25f + 0.25f));
            policeBox_CubeTop.Color = Color.blue;
            policeBox_CubeTop.Scale = new Vector3(0.28f, 0.2f, 0.28f);
            policeBox_CubeTop.Collidable = false;
            policeBox_CubeTop.Spawn();

            policeBox_Light = new LightSourceToy();
            policeBox_Light.LightColor = Color.white;
            policeBox_Light.LightShadows = true;
            policeBox_Light.LightIntensity = 1;
            policeBox_Light.LightRange=2;
            policeBox_Light.Position = ExteriorPosition + (Vector3.up * 5f);
            //  policeBox_Light.enabled = true;

            float forwardOffset = 0.75f;

            policeBox_Door = Primitive.Create(PrimitiveType.Cube);
            policeBox_Door.Position = ExteriorPosition + new Vector3(0,0, forwardOffset);
            policeBox_Door.Scale = new Vector3(1f, 4f, 0.1f);
            policeBox_Door.Color = Color.black;
            policeBox_Door.Collidable = false;
            policeBox_Door.Spawn();

            float windowHeight = 0.5f;
            float windowWidth = 0.5f;
            float windowDepth = 0.05f;

            float windowScaleFactor = 1.3f;
            policeBox_Window_Front_1 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Front_1.Position = ExteriorPosition + (new Vector3(-windowWidth / 2, 0, forwardOffset)) + (Vector3.up*(2.25f - 0.2f - windowHeight));
            policeBox_Window_Front_1.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Front_1.Color = Color.white;
            policeBox_Window_Front_1.Collidable = false;
            policeBox_Window_Front_1.Spawn();
            policeBox_Window_Front_2 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Front_2.Position = ExteriorPosition + (new Vector3(windowWidth/ 2, 0, forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Front_2.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Front_2.Color = Color.white;
            policeBox_Window_Front_2.Collidable = false;
            policeBox_Window_Front_2.Spawn();

            policeBox_Window_Back_1 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Back_1.Position = ExteriorPosition + (new Vector3(-windowWidth / 2, 0, -forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Back_1.Scale = new Vector3(windowWidth / windowScaleFactor , windowHeight, windowDepth);
            policeBox_Window_Back_1.Color = Color.white;
            policeBox_Window_Back_1.Collidable = false;
            policeBox_Window_Back_1.Spawn();
            policeBox_Window_Back_2 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Back_2.Position = ExteriorPosition + (new Vector3( windowWidth / 2, 0, -forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Back_2.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Back_2.Color = Color.white;
            policeBox_Window_Back_2.Collidable = false;
            policeBox_Window_Back_2.Spawn();


            policeBox_Window_Left_1 = Primitive.Create(PrimitiveType.Cube);
            policeBox_Window_Left_1.Position = ExteriorPosition + (new Vector3(-0.8f, 0, -windowWidth / 2 )) + (Vector3.up * (2.25f - 0.2f - windowHeight));
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

            shipInterior_Door = Primitive.Create(PrimitiveType.Cube);
            shipInterior_Door.Color = Color.black;
            shipInterior_Door.Scale = new Vector3(0.2f, 4f, 1f);
            shipInterior_Door.Position = new Vector3(42.50f, 1014.11f, -32.168f);
            shipInterior_Door.Collidable = false;
            shipInterior_Door.Spawn();

        }

        public void moveTardisModel(Vector3 ExteriorPosition)
        {

            Color TardisExteriorColor = Color.blue;

            policeBox_Cube.Position = ExteriorPosition;
            policeBox_Cube.Color = TardisExteriorColor;
            policeBox_Cube.Scale = new Vector3(1.5f, 4.5f, 1.5f);
            policeBox_Cube.Collidable = true;
            policeBox_Cube.Spawn();

            float startHeight = 2.25f;
            policeBox_Cube2.Position = ExteriorPosition + (Vector3.up * (startHeight));
            policeBox_Cube2.Color = TardisExteriorColor;
            policeBox_Cube2.Scale = new Vector3(1.2f, 0.2f, 1.2f);
            policeBox_Cube2.Collidable = true;
            policeBox_Cube2.Spawn();

            policeBox_Cube23.Position = ExteriorPosition + (Vector3.up * (startHeight));
            policeBox_Cube23.Color = TardisExteriorColor;
            policeBox_Cube23.Scale = new Vector3(1f, 0.4f, 1f);
            policeBox_Cube23.Collidable = true;
            policeBox_Cube2.Spawn();

            policeBox_Cube3.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f));
            policeBox_Cube3.Color = TardisExteriorColor;
            policeBox_Cube3.Scale = new Vector3(0.5f, 0.1f, 0.5f);
            policeBox_Cube3.Collidable = false;
            policeBox_Cube3.Spawn();

            policeBox_Cube4.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f + 0.1f));
            policeBox_Cube4.Color = TardisExteriorColor;
            policeBox_Cube4.Scale = new Vector3(0.4f, 0.25f, 0.4f);
            policeBox_Cube4.Collidable = false;
            policeBox_Cube4.Spawn();

            policeBox_CubeLight.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f + 0.1f + 0.25f));
            policeBox_CubeLight.Color = Color.white;
            policeBox_CubeLight.Scale = new Vector3(0.25f, 0.25f, 0.25f);
            policeBox_CubeLight.Collidable = false;
            policeBox_CubeLight.Spawn();

            policeBox_CubeTop.Position = ExteriorPosition + (Vector3.up * (startHeight + 0.2f + 0.1f + 0.25f + 0.25f));
            policeBox_CubeTop.Color = Color.blue;
            policeBox_CubeTop.Scale = new Vector3(0.28f, 0.2f, 0.28f);
            policeBox_CubeTop.Collidable = false;
            policeBox_CubeTop.Spawn();

            policeBox_Light.LightColor = Color.white;
            policeBox_Light.LightShadows = true;
            policeBox_Light.LightIntensity = 1;
            policeBox_Light.LightRange = 2;
            policeBox_Light.Position = ExteriorPosition + (Vector3.up * 5f);
            //  policeBox_Light.enabled = true;

            float forwardOffset = 0.75f;

            policeBox_Door.Position = ExteriorPosition + new Vector3(0, 0, forwardOffset);
            policeBox_Door.Scale = new Vector3(1f, 4f, 0.1f);
            policeBox_Door.Color = Color.black;
            policeBox_Door.Collidable = false;
            policeBox_Door.Spawn();

            float windowHeight = 0.5f;
            float windowWidth = 0.5f;
            float windowDepth = 0.05f;

            float windowScaleFactor = 1.5f;
            policeBox_Window_Front_1.Position = ExteriorPosition + (new Vector3(-windowWidth / 2, 0, forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Front_1.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Front_1.Color = Color.white;
            policeBox_Window_Front_1.Collidable = false;
            policeBox_Window_Front_1.Spawn();
            policeBox_Window_Front_2.Position = ExteriorPosition + (new Vector3(windowWidth / 2, 0, forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Front_2.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Front_2.Color = Color.white;
            policeBox_Window_Front_2.Collidable = false;
            policeBox_Window_Front_2.Spawn();

            policeBox_Window_Back_1.Position = ExteriorPosition + (new Vector3(-windowWidth / 2, 0, -forwardOffset)) + (Vector3.up * (2.25f - 0.2f - windowHeight));
            policeBox_Window_Back_1.Scale = new Vector3(windowWidth / windowScaleFactor, windowHeight, windowDepth);
            policeBox_Window_Back_1.Color = Color.white;
            policeBox_Window_Back_1.Collidable = false;
            policeBox_Window_Back_1.Spawn();
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
                        Log.Info("Sent inside tardis");
                        pl.Teleport(TardisInteriorPosition);
                        continue;
                    }
                    if (Vector3.Distance(pl.Position, IntDoorPos) < radius)
                    {
                        Log.Info("Sent outside tardis");
                        pl.Teleport(ExtDoorPos + new Vector3(0, 0, 1.52f) + (Vector3.up * 0.2f));
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
        }
   
    }
}
