using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    private static string FILE_NAME = "player.save";
    private Vector3Surrogate START_POSITION = new Vector3Surrogate(-7.35f, -4.55f, 1f);
    
    public Vector3Surrogate PlayerPosition;

    public static string FileName { get { return FILE_NAME; } }


    public PlayerData() 
    {
        PlayerPosition = START_POSITION;
    }
}
