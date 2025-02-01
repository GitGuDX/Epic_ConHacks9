using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            if (Input.GetMouseButton(0))
            {
                ShootBullet();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void ShootBullet()
    {
        GameObject instBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Vector3 direction = transform.forward; // Shoot in the player's forward direction
        instBullet.GetComponent<Bullet>().move = direction;

        // Ignore collision between the player and the bullet
        Physics.IgnoreCollision(instBullet.GetComponent<Collider>(), GetComponent<Collider>());
    }
}