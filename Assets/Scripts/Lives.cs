using Fusion;
using UnityEngine;

public class Lives : NetworkBehaviour
{
    [SerializeField] 
    private float invulnerabilityDuration = 2f;
    public GameObject[] livesUI;

    [Networked]
    private NetworkBool isInvulnerable { get; set; }

    [Networked]
    public int lives {get; set;} = 3;
    private TickTimer invulnerabilityTimer;

    
    public override void FixedUpdateNetwork()
    {
        // If the invulnerability timer has expired, set isInvulnerable to false
        if (isInvulnerable && invulnerabilityTimer.Expired(Runner))
        {
            isInvulnerable = false;
        }
    }

    private void StartInvulnerability()
    {
        isInvulnerable = true;
        invulnerabilityTimer = TickTimer.CreateFromSeconds(Runner, invulnerabilityDuration);
    }

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
        if (isInvulnerable) return;

        lives--;
        if (lives <= 0) {
            Debug.Log("death");
            Destroy(gameObject);
        }
        UpdateLivesUI();
        StartInvulnerability();
    }
}
