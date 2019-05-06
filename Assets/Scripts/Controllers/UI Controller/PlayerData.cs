using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int numOfLastCheckpoint; //Unity no deja usar gameobjects para sistemas de guardado, así que habrá que darle un valor a cada checkpoint para poder saber cuál ha sido el último en ser usado por el jugador

    public PlayerData(PlayerModel player)
    {
        numOfLastCheckpoint = player.lastCheckpointChecked;
    }
}
