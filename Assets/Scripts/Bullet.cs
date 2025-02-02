using UnityEngine;
using Fusion;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : NetworkBehaviour
{
    private GunData gunData;
    public float speed = 10f;
    public float lifeTime;
    [Networked] 
    public Vector3 MovementDirection { get; set; }

    public void Initialize(GunData gunData)
        {
            lifeTime = gunData.lifeTime;
        }
    public override void Spawned()
    {
        // Set initial direction without authority check
        MovementDirection = transform.forward;
        
        // Destroy bullet after 5 seconds
        if (Object.HasStateAuthority)
        {
            StartCoroutine(DestroyAfterDelay());
        }
    }

    public override void FixedUpdateNetwork()
    {
        transform.position += MovementDirection * speed * Runner.DeltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Damage player
            other.GetComponent<Lives>().RPC_Damage();
            
            // Destroy bullet
            Runner.Despawn(Object);
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        if (Runner != null && Object != null)
        {
            Runner.Despawn(Object);
        }
    }
}