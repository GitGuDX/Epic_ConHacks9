using UnityEngine;
using Fusion;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : NetworkBehaviour
{
    public float speed = 20f;

    [Networked] 
    public Vector3 MovementDirection { get; set; }

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

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        if (Runner != null && Object != null)
        {
            Runner.Despawn(Object);
        }
    }
}