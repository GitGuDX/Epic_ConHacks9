using Fusion;
using UnityEngine;

public class Lives : NetworkBehaviour
{
    public GameObject[] livesUI;

    [Networked]
    public int lives {get; set;} = 3;

    public void UpdateLivesUI() {
        if (lives < 1) {
            livesUI[0].SetActive(false);
        }
        else if (lives < 2) {
            livesUI[1].SetActive(false);
        }
        else if (lives < 3) {
            livesUI[2].SetActive(false);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Damage() {
        lives--;
        if (lives <= 0) {
            Debug.Log("death");
            Destroy(gameObject);
        }
        UpdateLivesUI();
    }
}
