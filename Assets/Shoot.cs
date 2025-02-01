using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform camera;
    public Transform shootPoint;
    public float fireRate;
    public GameObject bullet;

    Vector3 mousePos;
    float nextFireTime;

    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time >= nextFireTime)
        {
            if(Input.GetMouseButton(0))
            {
                ShootBullet();
            }
        }

        // Update mouse position
        mousePos = Input.mousePosition;
        mousePos.z = camera.position.z - shootPoint.position.z;
        mousePos = camera.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
    }

    void ShootBullet()
    {
        GameObject instBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Vector3 direction = (mousePos - shootPoint.position).normalized;
        instBullet.GetComponent<Bullet>().move = direction;

        // Ignore collision between the player and the bullet
        Physics.IgnoreCollision(instBullet.GetComponent<Collider>(), GetComponent<Collider>());

        nextFireTime = Time.time + 1/fireRate;
    }
}