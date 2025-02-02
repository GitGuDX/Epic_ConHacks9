using UnityEngine;
using Fusion;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : NetworkBehaviour
{
    public float speed = 20f;

    private GunData currentGunData;

    [Networked] 
    public Vector3 MovementDirection { get; set; }

    public override void Spawned()
    {
        // Set initial direction without authority check
        MovementDirection = transform.forward;
        
        // Destroy bullet after lifetime
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
        yield return new WaitForSeconds(currentGunData.lifeTime);
        if (Runner != null && Object != null)
        {
            Runner.Despawn(Object);
        }
    }
}