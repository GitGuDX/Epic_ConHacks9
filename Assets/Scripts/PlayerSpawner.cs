using System.Linq;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public GameObject PlayerUI;
    public Transform playerOneSpawn;
    public Transform playerTwoSpawn;
    public Transform playerThreeSpawn;
    public Transform playerFourSpawn;


    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Vector3 spawnPosition;
            switch (Runner.ActivePlayers.Count())
            {
                case 1:
                    spawnPosition = playerOneSpawn.position;
                    break;
                case 2:
                    spawnPosition = playerTwoSpawn.position;
                    break;
                case 3:
                    spawnPosition = playerThreeSpawn.position;
                    break;
                case 4:
                    spawnPosition = playerFourSpawn.position;
                    break;
                default:
                    spawnPosition = playerOneSpawn.position; // Default to player one spawn if something goes wrong
                    break;
            }
            Runner.Spawn(PlayerPrefab, spawnPosition, Quaternion.identity);
            Instantiate(PlayerUI);
        }
    }
}
