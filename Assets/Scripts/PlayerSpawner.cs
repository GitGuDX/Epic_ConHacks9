using System;
using System.Linq;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public GameObject PlayerUI;
    public Transform[] playerSpawns;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab, playerSpawns[Runner.ActivePlayers.Count() - 1].position, Quaternion.identity);
            Instantiate(PlayerUI);
        }
    }
}
