using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public float fireRate = 3f; // Shots per second
    private float timeBetweenShots;
    private float nextShotTime;
    private AmmoSystem ammoSystem;

    void Start()
    {
        ammoSystem = GetComponent<AmmoSystem>();
        timeBetweenShots = 1f / fireRate;
        nextShotTime = Time.time;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextShotTime && ammoSystem.CanShoot())
        {
            ShootBullet();
            ammoSystem.UseAmmo();
            nextShotTime = Time.time + timeBetweenShots;
        }
    }

    protected virtual void ShootBullet()
    {
        GameObject instBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Vector3 direction = transform.forward;
        instBullet.GetComponent<Bullet>().move = direction;
        Physics.IgnoreCollision(instBullet.GetComponent<Collider>(), GetComponent<Collider>());
    }
}