using System.Linq;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public Transform playerOneSpawn;
    public Transform playerTwoSpawn;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab, Runner.ActivePlayers.Count() == 1 ? playerOneSpawn.position : playerTwoSpawn.position, Quaternion.identity);
        }
    }
}
