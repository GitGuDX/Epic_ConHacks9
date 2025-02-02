using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public Transform playerOneSpawn;
    public Transform playerTwoSpawn;

    [Networked]
    private bool isFirstPlayer { get; set; } = false;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(PlayerPrefab, isFirstPlayer ? playerOneSpawn.position : playerTwoSpawn.position, Quaternion.identity);
            isFirstPlayer = !isFirstPlayer;
        }
    }
}
