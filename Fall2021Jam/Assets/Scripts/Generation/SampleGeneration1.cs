using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGeneration1 : RoomGenerator
{
    [SerializeField] Room bossRoom;
    [SerializeField] Room berryRoom;
     public override void PostGeneration()
    {
        GenerateBossRoom();
        GenerateBerryRoom();
        GenerateBerryRoom();
    }

    private void GenerateBossRoom()
    {
        bool generateSuccess = GenerateRoomOnLeaf(bossRoom);
        if (!generateSuccess)
        {
            Debug.Log("bossRoom was not placed!");
        }
    }

    private void GenerateBerryRoom()
    {
        bool generateSuccess = GenerateRoomOnLeaf(berryRoom);
        if (!generateSuccess)
        {
            Debug.Log("berryRoom was not placed!");
        }
    }
}
